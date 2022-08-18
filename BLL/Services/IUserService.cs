using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using BLL.Services;
using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Exceptions;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Users;

namespace MarathonApp.BLL.Services
{

    public interface IUserService
    {
        Task RegisterOwnerAsync();
        Task RegisterAdminAsync(AdminOwnerRegisterModel model);
        Task RegisterAsync(RegisterModel model);
        Task SendConfirmEmailAgainAsync(LoginModel.LoginIn model);
        Task ChangePasswordAsync(ChangePasswordModel model);
        Task<LoginModel.LoginOut> LoginAsync(LoginModel.LoginIn model);
        Task<(User, IEnumerable<Claim>, IList<string>)> UserClaimsAndRolesAsync(string email, string password);
        Task<(IEnumerable<Claim>, IList<string>)> ClaimsAndRolesAsync(User user);
        (string, DateTime) CreateAccessToken(IEnumerable<Claim> claims);
        Task<LoginModel.LoginOut> BuildResponse(User user, IEnumerable<Claim> claims, IList<string> roles);
        Task<LoginModel.LoginOut> UseRefreshTokenAsync(LoginModel.RefreshIn model);
    }


    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        private IEmailService _emailService;
        private RoleManager<IdentityRole> _roleManager;
        private MarathonContext _context;
        private IRefreshTokenService _refreshTokenService;
        private IHttpContextAccessor _httpContext;

        public UserService(UserManager<User> userManager, IConfiguration configuration, IEmailService emailService, RoleManager<IdentityRole> roleManager, MarathonContext context, IRefreshTokenService refreshTokenService, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
            _roleManager = roleManager;
            _context = context;
            _refreshTokenService = refreshTokenService;
            _httpContext = httpContext;
        }

        public async Task<LoginModel.LoginOut> LoginAsync(LoginModel.LoginIn model)
        {
            var (user, claims, roleNames) = await UserClaimsAndRolesAsync(model.Email, model.Password);
            return await BuildResponse(user, claims, roleNames);
        }

        public async Task<(User, IEnumerable<Claim>, IList<string>)> UserClaimsAndRolesAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new HttpException($"Пользователя не существует.", HttpStatusCode.BadRequest);

            if (!user.EmailConfirmed)
                throw new HttpException($"Почта не подтверждена.", HttpStatusCode.BadRequest);

            if (!await _userManager.CheckPasswordAsync(user, password))
                throw new HttpException("Неверный пароль.", HttpStatusCode.BadRequest);

            var (claims, roles) = await ClaimsAndRolesAsync(user);

            return (user, claims, roles);
        }

        public async Task<(IEnumerable<Claim>, IList<string>)> ClaimsAndRolesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            return (claims, roles);
        }

        public (string, DateTime) CreateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AuthSettings:Key").Value));
            var expirationDateUtc = DateTime.Now.AddMinutes(5);

            var token = new JwtSecurityToken(
                issuer: "me",
                audience: "you",
                claims: claims,
                expires: expirationDateUtc,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenAsString, expirationDateUtc);
        }

        public async Task<LoginModel.LoginOut> BuildResponse(User user, IEnumerable<Claim> claims, IList<string> roles)
        {
            var (accessToken, accessExpireDate) = CreateAccessToken(claims);
            var refreshToken = _refreshTokenService.GenerateRefreshToken();
            var refreshExpireDate = await _refreshTokenService.AddAsync(new RefreshTokenModel.Add
            {
                UserId = user.Id.ToString(),
                Name = refreshToken,
            }, TimeSpan.FromMinutes(1400));


            return new LoginModel.LoginOut
            {
                AccessToken = accessToken,
                AccessTokenExpireUtc = accessExpireDate,
                RefreshToken = refreshToken,
                RefreshTokenExpireUtc = refreshExpireDate,
                UserId = new Guid(user.Id),
                Email = user.Email,
                Role = roles[0],
            };
        }

        public async Task<LoginModel.LoginOut> UseRefreshTokenAsync(LoginModel.RefreshIn model)
        {
            IsAccessTokenValid(model.AccessToken);

            var token = await _refreshTokenService.ByValueAsync<RefreshTokenModel.Get>(model.RefreshToken);

            if (token is null || token.ExpirationDateUtc <= DateTime.UtcNow)
                throw new HttpException("Срок действия refresh токена истёк.", HttpStatusCode.BadRequest);

            await _refreshTokenService.DeleteAsync(model.RefreshToken);
            var user = await _userManager.FindByIdAsync(token.UserId);
            var (claims, roleNames) = await ClaimsAndRolesAsync(user);

            return await BuildResponse(user, claims, roleNames);
        }

        public void IsAccessTokenValid(string accessToken)
        {
            var securityKey = _configuration.GetSection("AuthSettings:Key").Value;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "me",
                ValidAudience = "you",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new HttpException("Access токен невалиден.", HttpStatusCode.NotAcceptable);
        }

        public async Task Logout()
        {
            var userId = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);

            var refreshTokens = _context.RefreshTokens.AsNoTracking().Where(t => t.UserId == new Guid(userId.Value));
            _context.RefreshTokens.RemoveRange(refreshTokens);
            await _context.SaveChangesAsync();
        }

        public async Task RegisterOwnerAsync()
        {
            var owner = await _userManager.GetUsersInRoleAsync("Owner");
            if (owner.Count != 0)
                throw new HttpException("Owner already exist", HttpStatusCode.BadRequest);

            var identityUser = new User
            {
                Email = _configuration.GetSection("OwnerInfo:Username").Value,
                UserName = _configuration.GetSection("OwnerInfo:Username").Value
            };
            var password = _configuration.GetSection("OwnerInfo:Password").Value;

            var result = await _userManager.CreateAsync(identityUser, password);

            if (!result.Succeeded)
                throw new HttpException("Owner was not created", HttpStatusCode.BadRequest);

            await _userManager.AddToRoleAsync(identityUser, UserRolesModel.Owner);
        }

        public async Task RegisterAdminAsync(AdminOwnerRegisterModel model)
        {
            if (await _userManager.Users.AnyAsync(u => u.Email == model.Email))
                throw new HttpException("Admin already exist", HttpStatusCode.BadRequest);

            var identityUser = new User
            {
                Email = model.Email,
                UserName = model.Email
            };
            var password = model.Password;

            var result = await _userManager.CreateAsync(identityUser, password);

            if (!result.Succeeded)
                throw new HttpException("Admin was not created", HttpStatusCode.BadRequest);
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
                throw new HttpException("Passwords do not match", HttpStatusCode.BadRequest);

            if (await _userManager.Users.AnyAsync(u => u.Email == model.Email))
                throw new HttpException("User already exist", HttpStatusCode.BadRequest);

            var identityUser = new User
            {
                Email = model.Email,
                UserName = model.Email
            };

            identityUser.Document = new Document();

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (!result.Succeeded)
            {
                throw new HttpException("User was not created", HttpStatusCode.BadRequest);
            }

            try
            {
                await _emailService.SendConfirmEmailAsync(identityUser);
            }

            catch(Exception ex)
            {
                throw new HttpException("Something wrong with email", ex.InnerException, HttpStatusCode.BadRequest);
            }

            if (!await _roleManager.RoleExistsAsync(UserRolesModel.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRolesModel.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRolesModel.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRolesModel.User));
            if (!await _roleManager.RoleExistsAsync(UserRolesModel.Owner))
                await _roleManager.CreateAsync(new IdentityRole(UserRolesModel.Owner));

            await _userManager.AddToRoleAsync(identityUser, UserRolesModel.User);
        }

        public async Task SendConfirmEmailAgainAsync(LoginModel.LoginIn model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new HttpException("There is no such user", HttpStatusCode.BadRequest);

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Неверный пароль.", HttpStatusCode.BadRequest);

            if (user.EmailConfirmed)
                throw new HttpException("Email is already confirmed", HttpStatusCode.BadRequest);

            try
            {
                await _emailService.SendConfirmEmailAsync(user);
            }

            catch(Exception ex)
            {
                throw new HttpException("Something wrong with email", ex.InnerException, HttpStatusCode.BadRequest);
            }
        }

        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new HttpException("There is no such user", HttpStatusCode.BadRequest);

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Неверный пароль.", HttpStatusCode.BadRequest);

            if (model.NewPassword != model.ConfirmPassword)
                throw new HttpException("Passwords do not match", HttpStatusCode.BadRequest);

            var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

            if (!result.Succeeded)
                throw new HttpException("Password was not changed", HttpStatusCode.BadRequest);
        }
    }
}

