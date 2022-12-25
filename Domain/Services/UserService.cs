using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Common.Options;
using Domain.Entities.Documents;
using Domain.Entities.Statuses;
using Domain.Entities.Users;
using Domain.Entities.Users.Exceptions;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Services.Interfaces;
using Domain.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Services;

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

    public void IsAccessTokenValid(string accessToken)
    {
        var key = _securityTokenOptions.Secret;
        var issuer = _securityTokenOptions.ValidIssuer;
        var audience = _securityTokenOptions.ValidAudience;
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
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

    public User CreateUser(string email) {
        var user = new User
        {
            Email = email,
            UserName = email
        };
        user.Document = new Document();
        user.Status = new Status();
        return user;
    }

    public void IsEmailConfirmed(User user)
    {
        if (user.EmailConfirmed == false) {
            throw new EmailWasNotConfirmedException();
        }
    }

    public void SetDateOfConfirmation(User user)
    {
        if (user.DateOfConfirmation == null) {
            user.DateOfConfirmation = DateTime.UtcNow;
        }
    }
}


