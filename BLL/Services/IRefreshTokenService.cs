using System;
using System.Security.Cryptography;
using DAL.Entities;
using Mapster;
using MarathonApp.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
        Task<DateTime> AddAsync<TModel>(TModel model, TimeSpan delta);
        Task<TModel> ByValueAsync<TModel>(string refreshToken) where TModel : class;
        Task DeleteAsync(string refreshToken);

    }

    public class RefreshTokenService : IRefreshTokenService
    {
        private MarathonContext _context;

        public RefreshTokenService(MarathonContext context)
        {
            _context = context;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<DateTime> AddAsync<TModel>(TModel model, TimeSpan delta)
        {
            var entity = model.Adapt<RefreshToken>();
            SetDates(entity, delta);
            _context.RefreshTokens.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ExpirationDateUtc;
        }

        private void SetDates(RefreshToken refreshToken, TimeSpan delta)
        {
            var now = DateTime.UtcNow;
            refreshToken.CreatedDateUtc = now;
            refreshToken.ExpirationDateUtc = now + delta;
            refreshToken.LoginProvider = "_";
        }

        public async Task<TModel> ByValueAsync<TModel>(string refreshToken)
            where TModel : class
        {
            var entity = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Name == refreshToken);

            if (entity is null)
                return null;

            return entity.Adapt<TModel>();
        }

        public async Task DeleteAsync(string refreshToken)
        {
            var entity = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Name == refreshToken);
            if (entity == null)
                throw new Exception($"Refresh token with value = '{refreshToken}' doesn't exists.");

            _context.Set<RefreshToken>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

