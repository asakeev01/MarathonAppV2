using System;
using Domain.Entities.Users;

namespace Domain.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        RefreshToken GenerateRefreshToken(long userId);
        public void IsRefreshTokenValid(RefreshToken refreshToken);
    }
}

