using System;
using System.Net;
using System.Text;
using Domain.Common.Options;
using Domain.Entities.Users;
using Domain.Entities.Users.Exceptions;
using Domain.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Domain.Services
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

        public async Task SendConfirmEmailAsync(User identityUser)
        {
            if (identityUser.EmailConfirmed)
                throw new EmailAlreadyConfirmedException();

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

            var validToken = WebEncodeToken(token);

            string url = $"{_appOptions.BackUrl}/api/auth/confirmemail?email={identityUser.Email}&token={validToken}";

            await SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Marathon App</h1>" + $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
        }

        public async Task SendResetPasswordEmailAsync(User identityUser, string email)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);

            var validToken = WebEncodeToken(token);

            string url = $"{_appOptions.BackUrl}/user/changePassword?email={email}&token={validToken}";

            await SendEmailAsync(email, "Reset your password", $"<h1>Marathon App</h1>" + $"<p>To reset your password <a href='{url}'>Clicking here</a></p>");
        }

        public async Task ForgetPasswordAsync(User user, string email)
        {
            await SendResetPasswordEmailAsync(user, email);
        }

        public async Task ConfirmEmailAsync(User user, string token)
        {
            var normalToken = WebDecodeToken(token);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (!result.Succeeded)
                throw new WrongTokenException();
        }

        public async Task ResetPasswordAsync(User user, string token, string newPassword)
        {
            var normalToken = WebDecodeToken(token);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, newPassword);

            if (!result.Succeeded)
                throw new WrongTokenException();
        }

        public string WebEncodeToken(string token)
        {
            //var encodedToken = Encoding.UTF8.GetBytes(token);

            //var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            var validToken = WebUtility.UrlEncode(token);

            return validToken;
        }

        public string WebDecodeToken(string token)
        {
            //var decodedToken = WebEncoders.Base64UrlDecode(token);

            //var normalToken = Encoding.UTF8.GetString(decodedToken);

            var normalToken = WebUtility.UrlDecode(token);

            return normalToken;
        }
    }
}

