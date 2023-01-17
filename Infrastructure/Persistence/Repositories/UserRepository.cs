using System;
using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Users;
using Domain.Entities.Users.Exceptions;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private UserManager<User> _userManager;
        private AppDbContext _repositoryContext;
        private IStringLocalizer<SharedResource> _localizer;

        public UserRepository(UserManager<User> userManager, AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
        {
            _userManager = userManager;
            _repositoryContext = repositoryContext;
            _localizer = localizer;
        }

        public async Task<bool> IsUserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
           
            if (user != null)
                return true;
            return false;
        }

        public async Task CreateUserAsync(User user, string password)
        {
            if(await _userManager.FindByEmailAsync(user.Email) != null)
                throw new UserAlreadyExistsException(_localizer);
            await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetByEmailAsync(string? email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new UserDoesNotExistException(_localizer);

            return user;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UserDoesNotExistException(_localizer);
            return user;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            if (user.EmailConfirmed)
                throw new EmailAlreadyConfirmedException(_localizer);

            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return emailToken;
        }

        public async Task ConfirmEmailAsync(User user, string emailToken)
        {
            if (user.EmailConfirmed)
                throw new EmailAlreadyConfirmedException(_localizer);

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);

            if (!result.Succeeded)
                throw new InvalidTokenException(_localizer);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            return passwordToken;
        }

        public async Task ResetPasswordAsync(User user, string passwordToken, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, passwordToken, newPassword);

            if (!result.Succeeded)
                throw new InvalidTokenException(_localizer);
        }

        public async Task AddToRoleAsync(User user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
                throw new WrongRoleException(_localizer);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task CheckPasswordAsync(User user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);


            if (!result)
                throw new WrongPasswordException(_localizer);
        }

        public async Task ChangePasswordAsync(User user, string password, string newPassword)
        {
            await _userManager.ChangePasswordAsync(user, password, newPassword);
        }

        public async Task UpdateAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}

