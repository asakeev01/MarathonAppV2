using System;
using System.Net;
using System.Text;
using Domain.Common.Options;
using Domain.Entities.Users;
using Domain.Entities.Users.Exceptions;
using Domain.Common.Contracts;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;
using Domain.Entities.Emails;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private AppUrlOptions _appOptions;
    private UserManager<User> _userManager;
    private readonly IUnitOfWork _unit;

    public EmailService(UserManager<User> userManager, IOptionsMonitor<AppUrlOptions> appOptions, IUnitOfWork unit)
    {
        _userManager = userManager;
        _appOptions = appOptions.CurrentValue;
        _unit = unit;   
    }

    public async Task SendEmailAsync(string toEmail, string subject, string content)
    {
        var mail = new Email()
        {
            Recipient = toEmail,
            Subject = subject,
            Content = content
        };
        await _unit.EmailRepository.CreateAsync(mail, save: true);
    }

    public async Task SendConfirmEmailAsync(string email, string emailToken)
    {
        var validToken = WebEncodeToken(emailToken);

        string url = $"{_appOptions.FrontUrl}/user/register/confirmEmail?email={email}&token={validToken}";

        await SendEmailAsync(email, "Confirm your email", $"<h1>Marathon App</h1>" + $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
    }

    public async Task SendPasswordResetTokenAsync(string email, string passwordToken)
    {
        var validToken = WebEncodeToken(passwordToken);

        string url = $"{_appOptions.FrontUrl}/user/changePassword?email={email}&token={validToken}";

        await SendEmailAsync(email, "Reset your password", $"<h1>Marathon App</h1>" + $"<p>To reset your password <a href='{url}'>Clicking here</a></p>");
    }

    public async Task SendStarterKitCodeAsync(string email, string starterKitCode)
    {
        await SendEmailAsync(email, "Starter Kit Code", $"<h1>Marathon App</h1>" + $"<p>{starterKitCode}</p>");
    }

    public string WebEncodeToken(string token)
    {
        var validToken = WebUtility.UrlEncode(token);

        return validToken;
    }

    public string WebDecodeToken(string token)
    {
        var normalToken = WebUtility.UrlDecode(token);

        return normalToken;
    }
}

