using System;
using System.Net;
using System.Text;
using System.Web;
using MailKit.Net.Smtp;
using MarathonApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace MarathonApp.BLL.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
        Task SendConfirmEmailAsync(User identityUser);
        Task ForgetPasswordEmailAsync(User identityUser, string email);
    }


    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;
        private UserManager<User> _userManager;

        public EmailService(UserManager<User> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;

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

            string emailAddress = _configuration.GetSection("MailSettings:Mail").Value;
            string password = _configuration.GetSection("MailSettings:Password").Value;

            SmtpClient client = new SmtpClient();

            try
            {
                await client.ConnectAsync("smtp.office365.com", 587, false);
                await client.AuthenticateAsync(emailAddress, password);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public async Task SendConfirmEmailAsync(User identityUser)
        {
            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

            //var validEmailToken = WebUtility.UrlEncode(confirmEmailToken);

            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(confirmEmailToken);

            var validEmailToken = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

            string url = $"{_configuration.GetSection("AppUrl").Value}/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

            await SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Marathon App</h1>" + $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
        }

        public async Task ForgetPasswordEmailAsync(User identityUser, string email)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);

            var encodedToken = Encoding.UTF8.GetBytes(token);

            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration.GetSection("AppUrl").Value}/api/auth/resetpassword?email={email}&token={validToken}";

            await SendEmailAsync(email, "Reset your password", $"<h1>Marathon App</h1>" + $"<p>To reset your password <a href='{url}'>Clicking here</a></p>");
        }
    }
}