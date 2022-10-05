using System;
using Domain.Entities.Users;

namespace Domain.Common.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task UserExistsAsync(string email);
        Task CreateUserAsync(User user, string password);
        Task<User> GetByEmailAsync(string email);
        Task ConfirmEmailAsync(User user, string token);
    }
}

