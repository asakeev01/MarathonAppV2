using System;
using System.Net;
using System.Text;
using System.Web;
using MailKit.Net.Smtp;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Exceptions;
using MarathonApp.Models.Users;
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
        Task SendResetPasswordEmailAsync(User identityUser, string email);
        Task ConfirmEmailAsync(string userIs, string token);
        Task ForgetPasswordAsync(string email);
        Task ResetPasswordAsync(ResetPasswordModel model);
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
                throw new HttpException(ex.Message, HttpStatusCode.InternalServerError);
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
                throw new HttpException($"Email '{identityUser.Email}' arleady confirmed", HttpStatusCode.BadRequest);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

            var validToken = WebEncodeToken(token);

            string url = $"{_configuration.GetSection("AppUrl").Value}/api/auth/confirmemail?userid={identityUser.Id}&token={validToken}";

            await SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Marathon App</h1>" + $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
        }

        public async Task SendResetPasswordEmailAsync(User identityUser, string email)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);

            var validToken = WebEncodeToken(token);

            string url = $"{_configuration.GetSection("AppUrl").Value}/user/changePassword?email={email}&token={validToken}";

            await SendEmailAsync(email, "Reset your password", $"<h1>Marathon App</h1>" + $"<p>To reset your password <a href='{url}'>Clicking here</a></p>");
        }

        public async Task ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new HttpException($"Пользователя не существует.", HttpStatusCode.BadRequest);

            await SendResetPasswordEmailAsync(user, email);
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new HttpException($"Пользователя не существует.", HttpStatusCode.BadRequest);

            var normalToken = WebDecodeToken(token);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (!result.Succeeded)
                throw new HttpException("Wrong token or user does not exist.", HttpStatusCode.BadRequest);
        }

        public async Task ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                throw new HttpException($"Пользователя не существует.", HttpStatusCode.BadRequest);

            if (model.NewPassword != model.ConfirmPassword)
                throw new HttpException("Пароли не сходятся.", HttpStatusCode.BadRequest);

            var normalToken = WebDecodeToken(model.Token);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (!result.Succeeded)
                throw new HttpException("Wrong token or user does not exist.", HttpStatusCode.BadRequest);
        }

        public string WebEncodeToken(string token)
        {
            var encodedToken = Encoding.UTF8.GetBytes(token);

            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            return validToken;
        }

        public string WebDecodeToken(string token)
        {
            var decodedToken = WebEncoders.Base64UrlDecode(token);

            var normalToken = Encoding.UTF8.GetString(decodedToken);

            return normalToken;
        }
    }
}