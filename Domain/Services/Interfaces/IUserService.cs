using System;
using System.Security.Claims;
using Domain.Entities.Users;
using Domain.Services.Models;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        //Task SendConfirmEmailAgainAsync(LoginModel.LoginIn model);
        //Task ChangePasswordAsync(ChangePasswordModel model);
        LoginOut LoginAsync(User user, RefreshToken refreshToken, IList<string> roles);
        void IsAccessTokenValid(string accessToken);
        //Task<(User, IEnumerable<Claim>, IList<string>)> UserClaimsAndRolesAsync(string email, string password);
        //Task<LoginModel.LoginOut> BuildResponse(User user, IEnumerable<Claim> claims, IList<string> roles);
        //Task<LoginModel.LoginOut> UseRefreshTokenAsync(LoginModel.RefreshIn model);
    }
}

