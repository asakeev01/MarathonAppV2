using System;
using Domain.Entities.Users;

namespace Domain.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        RefreshToken GenerateRefreshToken(long userId);
        public void IsRefreshTokenValid(RefreshToken refreshToken);
        //Task<DateTime> AddAsync<TModel>(TModel model, TimeSpan delta);
        //Task<TModel> ByValueAsync<TModel>(string refreshToken) where TModel : class;
        //Task DeleteAsync(string refreshToken);
    }
}

