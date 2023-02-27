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

        string url = $"{_appOptions.FrontUrl}user/register/confirmEmail?email={email}&token={validToken}";

        await SendEmailAsync(email, "Confirm your email on Run the Silk Road", $"<h1>Run the Silk Road</h1>" 
            + $"<p>Физкульт-привет! Ваш личный кабинет почти готов.Для завершения процесса регистрации пройдите по <a href='{url}'> ссылке </a>. Не забудьте заполнить профиль и отправить его на верификацию операторам. Верификация профиля позволит Вам получить стартовый пакет без предъявления документов, не носить с собой справку и передать право получения стартового пакета третьему лицу.</p>"
            + $"<p><br><br>Hi, there! Your account is almost ready. Please, verify your e-mail address. <br><a href='{url}'>Link</a>. <br>When you get access to your account we recommend to fill in your profile information.<br>Verified account have advanced options such as to get your participant package without any ID, or get a package pick up code and ask your friend to pick your package.</p>"
            + $"<p><br><br>Салам!<br>Сиздин өздүк кабинетиниз дээрлик даяр. Каттоо процессин аяктоо үчүн төмөнкү шилтемеге өтүңүз - <br><a href='{url}'>шилтемеге</a>.<br>Профилди толтуруп, аны операторлорго текшерүүгө жөнөтүүнү унутпаңыз.<br>Профилди текшерүү документтерди көрсөтпөстөн старттык пакетти алууга мүмкүндүк берет,<br>справканы өзү менен алып жүрбөө жана старттык пакетти алуу укугун үчүнчү жакка өткөрүп берүү.</p>"
            );
    }

    public async Task SendPasswordResetTokenAsync(string email, string passwordToken)
    {
        var validToken = WebEncodeToken(passwordToken);

        string url = $"{_appOptions.FrontUrl}user/changePassword?email={email}&token={validToken}";

        await SendEmailAsync(email, "Reset your password on Run the Silk Road", $"<h1>Run the Silk Road</h1>"
            + $"<p>Физкульт-привет! Кто-то, возможно Вы, пытается сбросить пароль на сайте my.runthesilkroad.com. Если это были Вы, перейдите по <a href='{url}'>ссылке</a>. Если это были не Вы ничего делать не нужно.<p>"
            + $"<p><br><br>Hi, there! Someone, may be you, is trying to reset password on my.runthesilkroad.com site. If that was you click this <a href='{url}'>link</a>. If it was not you, just don't do anything.<p>"
            + $"<p><br><br>Салам! Кимдир бирөө, балким, сиз my.runthesilkroad.com веб-сайтта паролду калыбына келтирүүгө аракет кылып жатасыз.  Эгер сиз болсоңуз, <a href='{url}'>шилтемеге</a> өтүңүз. Эгер сиз болбосоңуз эч нерсе кылуунун кажети жок.<p>"

            );
    }

    public async Task SendStarterKitCodeAsync(string email, string starterKitCode)
    {
        await SendEmailAsync(email, "Starter Kit Code on Run the Silk Road", $"<h1>Run the Silk Road</h1>"
            + $"<p>Физкульт-привет! Ваш код для получения стартового пакета - {starterKitCode}. Он нужен, чтобы передать право получения стартового пакета третьему лицу. Пересылайте его аккуратно, все, кому он будет доступен смогут получить Ваш стартовый пакет. Мы зафиксируем данные реального получателя.<p>"
            + $"<p><br><br>Hi, there! Your start package pick up code is - { starterKitCode}.Youк friend can use this code to pick up your start package instead of you. Be careful, anyone who have this code can take your package from us.We are going to record receiver's personal information<p>"
            + $"<p><br><br>Салам! Старттык пакетти алуу үчүн сиздин кодуңуз - {starterKitCode}. Ал баштапкы пакетти алуу укугун үчүнчү жакка өткөрүп берүү үчүн керек. Аны тыкан жөнөтүңүз, ал жеткиликтүү боло турган бардык адамдар баштапкы пакетиңизди ала алышат. Чыныгы алуучунун маалыматтарын камтыйбыз.<p>"
            );
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

