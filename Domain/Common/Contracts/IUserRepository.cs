using Domain.Entities.Users;

namespace Domain.Common.Contracts;

public interface IUserRepository : IBaseRepository<User>
{
    Task UserExistsAsync(string email);
    Task<bool> IsUserExistsAsync(string email);
    Task CreateUserAsync(User user, string password);
    Task<User> GetByEmailAsync(string? email);
    Task<User> GetByIdAsync(string userId);
    Task<string> GenerateEmailConfirmationTokenAsync(User user);
    Task ConfirmEmailAsync(User user, string token);
    Task<string> GeneratePasswordResetTokenAsync(User user);
    Task ResetPasswordAsync(User user, string passwordToken, string newPassword);
    Task AddToRoleAsync(User user, string role);
    Task<IList<string>> GetRolesAsync(User user);
    Task CheckPasswordAsync(User user, string password);
    Task ChangePasswordAsync(User user, string password, string newPassword);
    Task UpdateAsync(User user);
}
