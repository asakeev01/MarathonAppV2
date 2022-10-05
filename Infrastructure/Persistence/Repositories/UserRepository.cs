using System;
using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
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

        public UserRepository(UserManager<User> userManager, AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
        {
            _userManager = userManager;
            _repositoryContext = repositoryContext;
        }

        public async Task UserExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
                throw new UserAlreadyExistsException();
        }

        public async Task CreateUserAsync(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new UserAlreadyExistsException();
            return user;
        }

        public async Task ConfirmEmailAsync(User user, string token)
        {
            if (user.EmailConfirmed)
                throw new EmailAlreadyConfirmedException();

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
                throw new WrongTokenException();
        }
    }
}

