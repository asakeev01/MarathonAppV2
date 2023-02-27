using Domain.Common.Contracts;
using EmailServiceWorker.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailServiceWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private EmailOptions _emailOptions;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IOptionsMonitor<EmailOptions> emailOptions)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _emailOptions = emailOptions.CurrentValue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<IUnitOfWork>();


            var mail = await context.EmailRepository.GetFirstOrDefaultAsync(orderBy: source => source.OrderBy(x => x.Attempts).ThenBy(x => x.Id));

            if (mail != null)
            {
                _logger.LogInformation("Email to {0}, attempt = {1}", mail.Recipient, mail.Attempts);

                if (mail.Attempts >= 10)
                {
                    _logger.LogInformation("Email to {0}, attempt = {1}. Attempt greater then 10, deleting message.", mail.Recipient, mail.Attempts);
                    await context.EmailRepository.Delete(mail, save: true);
                }
                else
                {
                    MimeMessage message = new MimeMessage();

                    message.From.Add(new MailboxAddress("Run the Silk Road", "chasefy_office@timelysoft.net"));

                    message.To.Add(MailboxAddress.Parse(mail.Recipient));

                    message.Subject = mail.Subject;

                    message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = mail.Content
                    };

                    SmtpClient client = new SmtpClient();

                    try
                    {
                        await client.ConnectAsync("smtp.office365.com", 587, false);
                        await client.AuthenticateAsync(_emailOptions.Mail, _emailOptions.Password);
                        await client.SendAsync(message);
                        _logger.LogInformation("Email sended to {recipient}, date - {date}, attempt = {attempt}", mail.Recipient, DateTimeOffset.Now, mail.Attempts);
                        await context.EmailRepository.Delete(mail, save: true);

                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation(ex.Message);
                        _logger.LogInformation("Cant send message {time} to {email}, attempts - {attempt}", DateTimeOffset.Now, mail.Recipient, mail.Attempts);
                        mail.Attempts += 1;
                        await context.EmailRepository.SaveAsync();
                    }

                    finally
                    {
                        client.Disconnect(true);
                        client.Dispose();
                    }
                }
            }
            context.Dispose();
            await Task.Delay(10000, stoppingToken);
        }
    }
}