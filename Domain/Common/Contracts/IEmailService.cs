using System;
using Domain.Entities.Users;

namespace Domain.Common.Contracts;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string content);
    Task SendConfirmEmailAsync(string email, string emailToken);
    Task SendPasswordResetTokenAsync(string email, string emailToken);
    Task SendStarterKitCodeAsync(string email, string starterKitCode);
    Task SendConfirmEmailAdminAsync(string email, string emailToken, string login, string password);
    string WebEncodeToken(string token);
    string WebDecodeToken(string token);
}

