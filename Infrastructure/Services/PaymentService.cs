using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Core.UseCases.Payments.Commands.ReceivePayment;
using Domain.Common.Contracts;
using Domain.Common.Options;
using Domain.Common.Resources;
using Domain.Entities.Applications;
using Domain.Entities.Applications.Exceptions;
using Domain.Services.Models;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.Xml;

namespace Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unit;
    private PaymentOptions _paymentOptions;
    private AppUrlOptions _appOptions;
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(ILogger<PaymentService> logger, IUnitOfWork unit, IOptionsMonitor<PaymentOptions> paymentOptions, IOptionsMonitor<AppUrlOptions> appOptions, IStringLocalizer<SharedResource> _localizer)
    {
        _unit = unit;
        _paymentOptions = paymentOptions.CurrentValue;
        _appOptions = appOptions.CurrentValue;
        _logger = logger;
        this._localizer = _localizer;

    }

    public async Task<Application> SendInitPaymentAsync(Application application)
    {
        try
        {
            var user = application.User;
            int merchant_id = _paymentOptions.MerchantId;
            int lifetime = _paymentOptions.PgLifetime;
            int amount = (int)application.Price;
            string user_phone = user.PhoneNumber;
            string user_contact_email = user.Email;
            string url = _paymentOptions.Url;
            string init = _paymentOptions.InitPaymentUrl;
            string description = "Payment";
            string salt = "Random";
            string order_id = application.Id.ToString();
            string result_url = _appOptions.BackUrl + _appOptions.ReceivePaymentUrl;
            string secret_key = _paymentOptions.SecretKey;
            string text = init + ";" + amount + ";" + description + ";" + lifetime + ";" + merchant_id + ";" + order_id + ";" + result_url + ";" +
                salt + ";" + user_contact_email + ";" + secret_key;


            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] resultBytes = Encoding.UTF8.GetBytes(text);

            resultBytes = md5.ComputeHash(resultBytes);

            StringBuilder sig = new StringBuilder();
            foreach (byte ba in resultBytes)
            {
                sig.Append(ba.ToString("x2").ToLower());
            }

            var client = new RestClient(url);
            var request = new RestRequest(init);
            var dotNetXmlDeserializer = new DotNetXmlDeserializer();
            request.AddHeader("Content-type", "application/json");

            request.AddJsonBody(new InitPaymentRequest
            {
                pg_order_id = order_id,
                pg_merchant_id = merchant_id,
                pg_amount = amount,
                pg_description = description,
                pg_result_url = result_url,
                pg_salt = salt,
                pg_sig = sig.ToString(),
                pg_lifetime = lifetime,
                //pg_user_phone = user_phone,
                pg_user_contact_email = user_contact_email
            });

            var response = await client.PostAsync(request);
            var data = dotNetXmlDeserializer.Deserialize<InitPaymentResponse>(response);

            if (data.pg_status != "ok")
                throw new PaymentNotInitializedException(_localizer);

            application.PaymentId = data.pg_payment_id;
            application.PaymentUrl = data.pg_redirect_url;
            await _unit.ApplicationRepository.Update(application, save: true);
        }
        catch (PaymentNotInitializedException ex)
        {
            var distance = application.Distance;
            distance.InitializedPlaces -= 1;
            await _unit.DistanceRepository.Update(distance, save: true);
            await _unit.ApplicationRepository.Delete(application, save: true);
            throw ex;
        }
        catch (Exception ex)
        {
            var distance = application.Distance;
            distance.InitializedPlaces -= 1;
            await _unit.DistanceRepository.Update(distance, save: true);
            await _unit.ApplicationRepository.Delete(application, save: true);
            throw ex;
        }
        return application;
    }

    public bool IsSignatureRight(ReceivePaymentDto receivePaymentDto)
    {
        string text;
        var receive_payment_url = _appOptions.ReceivePaymentUrl;
        var amount = receivePaymentDto.pg_amount;
        var currency = receivePaymentDto.pg_currency;
        var can_reject = receivePaymentDto.pg_can_reject;
        var description = receivePaymentDto.pg_description;
        var net_amount = receivePaymentDto.pg_net_amount;
        var order_id = receivePaymentDto.pg_order_id;
        var payment_id = receivePaymentDto.pg_payment_id;
        var ps_amount = receivePaymentDto.pg_ps_amount;
        var ps_full_amount = receivePaymentDto.pg_ps_full_amount;
        var ps_currency = receivePaymentDto.pg_ps_currency;
        var payment_date = receivePaymentDto.pg_payment_date;
        var payment_method = receivePaymentDto.pg_payment_method;
        var result = receivePaymentDto.pg_result;
        var salt = receivePaymentDto.pg_salt;
        var sig = receivePaymentDto.pg_sig;
        var testing_mode = receivePaymentDto.pg_testing_mode;
        var user_phone = receivePaymentDto.pg_user_phone;
        var user_contact_email = receivePaymentDto.pg_user_contact_email;
        var secret_key = _paymentOptions.SecretKey;

        if (payment_method == "bankcard")
        {
            var captured = receivePaymentDto.pg_captured;
            var card_id = receivePaymentDto.pg_card_id;
            var card_pan = receivePaymentDto.pg_card_pan;
            var card_exp = receivePaymentDto.pg_card_exp;
            var card_owner = receivePaymentDto.pg_card_owner;
            var card_brand = receivePaymentDto.pg_card_brand;

            text = receive_payment_url + ";" + amount + ";" + currency + ";" + can_reject + ";" + captured + ";" +
                card_id + ";" + card_pan + ";" + card_exp + ";" + card_owner + ";" + card_brand + ";" +
                description + ";" + net_amount + ";" + order_id + ";" +
                payment_id + ";" + ps_amount + ";" + ps_full_amount + ";" + ps_currency + ";" + payment_date + ";" + payment_method + ";" +
                result + ";" + salt + ";" + testing_mode + ";" + user_phone + ";" + user_contact_email + ";" + secret_key;
        }

        else
        {
            text = receive_payment_url + ";" + amount + ";" + currency + ";" + can_reject + ";" +
                description + ";" + net_amount + ";" + order_id + ";" +
                payment_id + ";" + ps_amount + ";" + ps_full_amount + ";" + ps_currency + ";" + payment_date + ";" + payment_method + ";" +
                result + ";" + salt + ";" + testing_mode + ";" + user_phone + ";" + user_contact_email + ";" + secret_key;
        }

        

        MD5 md5 = new MD5CryptoServiceProvider();

        byte[] resultBytes = Encoding.UTF8.GetBytes(text);

        resultBytes = md5.ComputeHash(resultBytes);

        StringBuilder sigBack = new StringBuilder();
        foreach (byte ba in resultBytes)
        {
            sigBack.Append(ba.ToString("x2").ToLower());
        }
        if (sigBack.ToString() != sig)
        {
            _logger.LogInformation(text);
            _logger.LogInformation(sig);
            Console.WriteLine(text);
            Console.WriteLine(sig);
            Console.WriteLine("Signature is not right");
            return false;
        }  
        return true;
    }
}


