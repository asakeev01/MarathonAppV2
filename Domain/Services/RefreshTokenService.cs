using System;
using System.Security.Cryptography;
using Domain.Entities.Users;
using Domain.Entities.Users.Exceptions;
using Domain.Services.Interfaces;

namespace Domain.Services;

public class RefreshTokenService : IRefreshTokenService
{
    public RefreshTokenService()
    {
    }

    public RefreshToken GenerateRefreshToken(long userId)
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Name = Convert.ToBase64String(randomNumber)
        };
        var expirationTime = TimeSpan.FromMinutes(1400);

        SetDates(refreshToken, expirationTime);

        return refreshToken;
    }

    private void SetDates(RefreshToken refreshToken, TimeSpan delta)
    {
        var now = DateTime.UtcNow;
        refreshToken.CreatedDateUtc = now;
        refreshToken.ExpirationDateUtc = now + delta;
        refreshToken.LoginProvider = "_";
    }

    public void IsRefreshTokenValid(RefreshToken refreshToken)
    {
        if (refreshToken.ExpirationDateUtc <= DateTime.UtcNow)
            throw new InvalidTokenException();
    }
}


