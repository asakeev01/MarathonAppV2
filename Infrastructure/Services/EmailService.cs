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

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private EmailOptions _emailOptions;
        private AppUrlOptions _appOptions;
        private UserManager<User> _userManager;

        public EmailService(UserManager<User> userManager, IOptionsMonitor<EmailOptions> emailOptions, IOptionsMonitor<AppUrlOptions> appOptions)
        {
            _userManager = userManager;
            _emailOptions = emailOptions.CurrentValue;
            _appOptions = appOptions.CurrentValue;

        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress("Admin", "chasefy_office@timelysoft.net"));

            message.To.Add(MailboxAddress.Parse(toEmail));

            message.Subject = subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = content
            };

            string emailAddress = _emailOptions.Mail;
            string password = _emailOptions.Password;

            SmtpClient client = new SmtpClient();

            try
            {
                await client.ConnectAsync("smtp.office365.com", 587, false);
                await client.AuthenticateAsync(emailAddress, password);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw new EmailServiceConnectionException();
            }

            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
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
}

