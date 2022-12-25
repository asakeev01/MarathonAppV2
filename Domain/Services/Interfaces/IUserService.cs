using System;
using System.Security.Claims;
using Domain.Entities.Documents;
using Domain.Entities.Users;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Services.Models;
using Domain.Entities.Statuses;

namespace Domain.Services.Interfaces;

public interface IUserService
{
    LoginOut LoginAsync(User user, RefreshToken refreshToken, IList<string> roles);
    void IsAccessTokenValid(string accessToken);
    User CreateUser(string email);
    void IsEmailConfirmed(User user);
    void SetDateOfConfirmation(User user);
}

