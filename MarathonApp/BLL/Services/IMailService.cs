using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace MarathonApp.BLL.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }


    public class MailService : IMailService
    {
        private IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}