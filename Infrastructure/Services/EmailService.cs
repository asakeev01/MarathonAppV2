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
using static System.Net.WebRequestMethods;

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

    public async Task SendConfirmEmailAdminAsync(string email, string emailToken, string login, string password)
    {
        var validToken = WebEncodeToken(emailToken);

        string url = $"{_appOptions.FrontUrl}user/register/confirmEmail?email={email}&token={validToken}";

        await SendEmailAsync(email, "Confirm your email on Run the Silk Road", $"<h1>Run the Silk Road</h1>"
            +  $"<p>На сайте my.runthesilkroad.com Вам назначена роль Менеджер/Волонтер.<br>Для подтверждения электронной почты<br>пройдите по <a href='{url}'> ссылке </a>.<br>Ваш логин: {login}<br>Ваш пароль: {password}<br>Вы можете сменить Ваш пароль через процедуру восстановления пароля<br>Данный доступ дает Вам возможность входа в административную часть сайта.<br>Пожалуйста будьте предельно аккуратны, система работает в боевом режиме, все внесенные Вами изменения отразятся в сохраненных данных<br>Если Вам нужна консультация по использованию системы обратитесь к администратору по адресу maxim.sim@gmail.com.<br></p>"
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

    public async Task SendStarterKitCodeAsync(string email, string starterKitCode, string name, string surname, string distance, string marathonDate, string distanceAge, string EnMarathon, string RusMarathon, string KgMarathon, string number)
    {
        await SendEmailAsync(email, "Подтверждение оплаты участника / Confirmation letter for marathon / Марафонго катышууну тастыктаган кат", $"<h1>Run the Silk Road</h1>"
            + $"<p>Здравствуйте {name} {surname}!<br>Спасибо, что выбрали Run the Silk Road!<br>Вы зарегистрированы на {RusMarathon}, который состоится {marathonDate}<br>Ваша дистанция: {distance}<br>Ваш стартовый номер: {number}<br>Ваша возрастная категория: {distanceAge}<br>Где и когда можно забрать стартовый пакет можно посмотреть <a href='http://runthesilkroad.com/startpack'>ссылке</a><br>Ваш код для получения стартового пакета - {starterKitCode}. Он нужен, чтобы передать право получения стартового пакета третьему лицу. Пересылайте его аккуратно, все, кому он будет доступен смогут получить Ваш стартовый пакет. Мы зафиксируем данные реального получателя.<br>Команда Run the Silk Road<br><p>"
            + $"<p><br><br>Hello, {name} {surname}!<br>Thanks for choosing Run the Silk Road!<br>You’ve just successfully registered for {EnMarathon}, which is on {marathonDate}<br>Your distance: {distance}<br>Your bib tag number: {number}<br>Your age category: {distanceAge}<br>Click this <a href='http://runthesilkroad.com/startpack'>link</a> to see details about when and where to pickup your start package<br>Your start package secret code is - {starterKitCode}. If you want someone to pickup your package instead of you, that person should have the code. Please, send it safely, we will give your package to whoever will say the code. However, we will record real recipient’s personal data.<br>Run the Silk Road team<br><p>"
            + $"<p><br><br>Саламатсызбы {name} {surname}!<br>Run the Silk Road марафонун тандаганыңыз үчүн рахмат!<br>Сиз {KgMarathon} чуркоо майрамына катталдыныз<br>Марафон {marathonDate} өтөт<br>Сиз тандаган аралыгы: {distance}<br>Сиздин старттык номериңиз:  {number}<br>Сиздин жаш категорияңыз: {distanceAge}<br>Старттык пакетти ала турган шарты сайтта түшүндүрүлөт <a href='http://runthesilkroad.com/startpack'>здесь</a><br>Старттык пакетти алуу үчүн сиздин кодуңуз - {starterKitCode}.  Ал баштапкы пакетти алуу укугун үчүнчү жакка өткөрүп берүү үчүн керек. Аны тыкан жөнөтүңүз, ал жеткиликтүү боло турган бардык адамдар баштапкы пакетиңизди ала алышат. Чыныгы алуучунун маалыматтарын камтыйбыз.<br>Run the Silk Road командасы<br><p>"
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

