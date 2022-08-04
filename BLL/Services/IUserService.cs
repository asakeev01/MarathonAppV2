using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Exceptions;
using MarathonApp.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MarathonApp.BLL.Services
{

    public interface IUserService
    {
        Task<UserManagerResponse> RegisterOwnerAsync();
        Task<UserManagerResponse> RegisterAdminAsync(AdminOwnerRegisterModel model);
        Task<UserManagerResponse> RegisterAsync(RegisterViewModel model);
        //Task<UserManagerResponse> LoginAsync(LoginViewModel model);
        Task<UserManagerResponse> ConfirmEmailAsync(string userIs, string token);
        Task<UserManagerResponse> ForgetPasswordAsync(string email);
        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<(User, IEnumerable<Claim>)> UserClaimsAsync(string email, string password);
        Task<IEnumerable<Claim>> ClaimsAsync(User user);
        Task<(string, DateTime)> CreateAccessToken(IEnumerable<Claim> claims);
    }


    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        private IEmailService _emailService;
        private RoleManager<IdentityRole> _roleManager;
        private MarathonContext _context;

        public UserService(UserManager<User> userManager, IConfiguration configuration, IEmailService emailService, RoleManager<IdentityRole> roleManager, MarathonContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<(User, IEnumerable<Claim>)> UserClaimsAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new HttpException($"Пользователя не существует.", HttpStatusCode.BadRequest);

            if (!user.EmailConfirmed)
                throw new HttpException($"Почта не подтверждена.", HttpStatusCode.BadRequest);

            if (!await _userManager.CheckPasswordAsync(user, password))
                throw new HttpException("Неверный пароль.", HttpStatusCode.BadRequest);

            var claims = await ClaimsAsync(user);

            return (user, claims);
        }

        public async Task<IEnumerable<Claim>> ClaimsAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            return claims;
        }

        public async Task<(string, DateTime)> CreateAccessToken(IEnumerable<Claim> claims)
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

        //public async Task<LoginViewModel>

        public void IsAccessTokenValid(string accessToken)
        {
            var securityKey = _configuration.GetSection("AuthSettins:Key").Value;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "me",
                ValidAudience = "you",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new HttpException("Access токен невалиден.", HttpStatusCode.NotAcceptable);
        }

        public async Task<UserManagerResponse> RegisterOwnerAsync()
        {
            var owner = await _userManager.GetUsersInRoleAsync("Owner");
            if (owner.Count != 0)
                return new UserManagerResponse
                {
                    Message = "Owner is already exist",
                    IsSuccess = false
                };

            var identityUser = new User
            {
                Email = _configuration.GetSection("OwnerInfo:Username").Value,
                UserName = _configuration.GetSection("OwnerInfo:Username").Value
            };
            var password = _configuration.GetSection("OwnerInfo:Password").Value;

            var result = await _userManager.CreateAsync(identityUser, password);


            if (result.Succeeded)
                await _userManager.AddToRoleAsync(identityUser, UserRolesModel.Owner);
                return new UserManagerResponse
                {
                    Message = "Owner was successfully registrated",
                    IsSuccess = true
                };
            return new UserManagerResponse
            {
                Message = "Owner is not created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> RegisterAdminAsync(AdminOwnerRegisterModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register form is empty");

            var identityUser = new User
            {
                Email = model.Email,
                UserName = model.Email
            };
            var password = model.Password;

            var result = await _userManager.CreateAsync(identityUser, password);


            if (result.Succeeded)
                await _userManager.AddToRoleAsync(identityUser, UserRolesModel.Admin);
            return new UserManagerResponse
            {
                Message = "Admin was successfully registered",
                IsSuccess = true
            };
            return new UserManagerResponse
            {
                Message = "Admin is not created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> RegisterAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register form is empty");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Passwords do not match",
                    IsSuccess = false
                };

            var identityUser = new User
            {
                Email = model.Email,
                UserName = model.Email
            };

            identityUser.Images = new ImagesEntity();

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            
            try
            {
                await _emailService.SendConfirmEmailAsync(identityUser);
            }
            catch
            {
                return new UserManagerResponse
                {
                    Message = "User was not created, something wrong with email",
                    IsSuccess = false
                };
            }

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(UserRolesModel.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRolesModel.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRolesModel.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRolesModel.User));
                if (!await _roleManager.RoleExistsAsync(UserRolesModel.Owner))
                    await _roleManager.CreateAsync(new IdentityRole(UserRolesModel.Owner));

                await _userManager.AddToRoleAsync(identityUser, UserRolesModel.User);

                


                return new UserManagerResponse
                {
                    Message = "User was successfully registrated",
                    IsSuccess = true
                };
            }
            return new UserManagerResponse
            {
                Message = "User is not created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> LoginAsync(LoginViewModel.LoginIn model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "There is no user with that email address",
                    IsSuccess = false
                };

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Incorrect password",
                    IsSuccess = false
                };

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AuthSettings:Key").Value));

            var token = new JwtSecurityToken(
                issuer: "me",
                audience: "you",
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                Expiration = token.ValidTo
            };
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false,
                };

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);

            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true
                };
            return new UserManagerResponse
            {
                Message = "Email was not confirmed",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "There is no user with that email address",
                    IsSuccess = false
                };

            await _emailService.ForgetPasswordEmailAsync(user, email);

            return new UserManagerResponse
            {
                Message = "Password reset URL has been sent to the email successfully!",
                IsSuccess = true
            };
        }

        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "There is no user with that email address",
                    IsSuccess = false
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Password do not match",
                    IsSuccess = false
                };

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);

            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true
                };
            return new UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}

