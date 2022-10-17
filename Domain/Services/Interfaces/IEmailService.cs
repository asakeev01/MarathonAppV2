using System;
using Domain.Entities.Users;

namespace Domain.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
        Task SendConfirmEmailAsync(string email, string emailToken);
        Task SendPasswordResetTokenAsync(string email, string emailToken);
        string WebEncodeToken(string token);
        string WebDecodeToken(string token);
    }
}

