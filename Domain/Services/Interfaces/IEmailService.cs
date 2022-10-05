using System;
using Domain.Entities.Users;

namespace Domain.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
        Task SendConfirmEmailAsync(User identityUser);
        Task SendResetPasswordEmailAsync(User identityUser, string email);
        Task ConfirmEmailAsync(User user, string token);
        Task ForgetPasswordAsync(User user, string email);
        Task ResetPasswordAsync(User user, string token, string newPassword);
        string WebEncodeToken(string token);
        string WebDecodeToken(string token);
    }
}

