using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Common.Options;
using Domain.Entities.Users;
using Domain.Entities.Users.Exceptions;
using Domain.Services.Interfaces;
using Domain.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private SecurityTokenOptions _securityTokenOptions;

        public UserService(IOptionsMonitor<SecurityTokenOptions> securityTokenOptions)
        {
            _securityTokenOptions = securityTokenOptions.CurrentValue;
        }

        public LoginOut LoginAsync(User user, RefreshToken refreshToken, IList<string> roles)
        {
            var claims = GetClaims(user, roles);

            return BuildResponse(user, refreshToken, claims, roles);
        }

        private IEnumerable<Claim> GetClaims(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            return claims;
        }

        private (string, DateTime) CreateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityTokenOptions.Secret));
            var expirationDateUtc = DateTime.Now.AddMinutes(_securityTokenOptions.ExpiresIn);
            var issuer = _securityTokenOptions.ValidIssuer;
            var audience = _securityTokenOptions.ValidAudience;

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expirationDateUtc,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenAsString, expirationDateUtc);
        }

        private LoginOut BuildResponse(User user, RefreshToken refreshToken, IEnumerable<Claim> claims, IList<string> roles)
        {
            var (accessToken, accessExpireDate) = CreateAccessToken(claims);

            return new LoginOut
            {
                AccessToken = accessToken,
                AccessTokenExpireUtc = accessExpireDate,
                RefreshToken = refreshToken.Name,
                RefreshTokenExpireUtc = refreshToken.ExpirationDateUtc,
                UserId = user.Id,
                Email = user.Email,
                Role = roles[0],
                Name = (user.Name == null && user.Surname == null ? null : user.Name + " " + user.Surname)
            };
        }

        //public async Task<LoginModel.LoginOut> UseRefreshTokenAsync(LoginModel.RefreshIn model)
        //{
        //    IsAccessTokenValid(model.AccessToken);

        //    var token = await _refreshTokenService.ByValueAsync<RefreshTokenModel.Get>(model.RefreshToken);

        //    if (token is null || token.ExpirationDateUtc <= DateTime.UtcNow)
        //        throw new HttpException("Срок действия refresh токена истёк.", HttpStatusCode.BadRequest);

        //    await _refreshTokenService.DeleteAsync(model.RefreshToken);
        //    var user = await _userManager.FindByIdAsync(token.UserId);
        //    var (claims, roleNames) = await ClaimsAndRolesAsync(user);

        //    return await BuildResponse(user, claims, roleNames);
        //}

        public void IsAccessTokenValid(string accessToken)
        {
            var key = _securityTokenOptions.Secret;
            var issuer = _securityTokenOptions.ValidIssuer;
            var audience = _securityTokenOptions.ValidAudience;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new InvalidTokenException();
        }

        //public async Task Logout()
        //{
        //    var userId = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);

        //    var refreshTokens = _context.RefreshTokens.AsNoTracking().Where(t => t.UserId == new Guid(userId.Value));
        //    _context.RefreshTokens.RemoveRange(refreshTokens);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task SendConfirmEmailAgainAsync(LoginModel.LoginIn model)
        //{
        //    var user = await _userManager.FindByEmailAsync(model.Email);
        //    if (user == null)
        //        throw new HttpException("There is no such user", HttpStatusCode.BadRequest);

        //    if (!await _userManager.CheckPasswordAsync(user, model.Password))
        //        throw new HttpException("Неверный пароль.", HttpStatusCode.BadRequest);

        //    if (user.EmailConfirmed)
        //        throw new HttpException("Email is already confirmed", HttpStatusCode.BadRequest);

        //    try
        //    {
        //        await _emailService.SendConfirmEmailAsync(user);
        //    }

        //    catch (Exception ex)
        //    {
        //        throw new HttpException("Something wrong with email", ex.InnerException, HttpStatusCode.BadRequest);
        //    }
        //}

        //public async Task ChangePasswordAsync(ChangePasswordModel model)
        //{
        //    var user = await _userManager.FindByEmailAsync(model.Email);
        //    if (user == null)
        //        throw new HttpException("There is no such user", HttpStatusCode.BadRequest);

        //    if (!await _userManager.CheckPasswordAsync(user, model.Password))
        //        throw new HttpException("Неверный пароль.", HttpStatusCode.BadRequest);

        //    if (model.NewPassword != model.ConfirmPassword)
        //        throw new HttpException("Passwords do not match", HttpStatusCode.BadRequest);

        //    var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

        //    if (!result.Succeeded)
        //        throw new HttpException("Password was not changed", HttpStatusCode.BadRequest);
        //}
    }
}

