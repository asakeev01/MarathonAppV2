using System;
using Domain.Entities.Users;

namespace Domain.Common.Contracts;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string content);
    Task SendConfirmEmailAsync(string email, string emailToken);
    Task SendPasswordResetTokenAsync(string email, string emailToken);
    Task SendStarterKitCodeAsync(string email, string starterKitCode, string name, string surname, string distance, string marathonDate, string distanceAge, string EnMarathon, string RusMarathon, string KgMarathon, string number);
    Task SendConfirmEmailAdminAsync(string email, string emailToken, string login, string password);
    string WebEncodeToken(string token);
    string WebDecodeToken(string token);
}

