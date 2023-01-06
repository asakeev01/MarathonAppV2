using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Domain.Common.Options;
using Domain.Common.Resources;
using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Applications.Exceptions;
using Domain.Entities.Distances;
using Domain.Entities.Users;
using Domain.Entities.Vouchers;
using Domain.Entities.Vouchers.Exceptions;
using Domain.Services.Interfaces;
using Domain.Services.Models;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.Xml;

namespace Domain.Services;

public class ApplicationService : IApplicationService
{
    private readonly IStringLocalizer<AccountResource> _localizer;
    private PaymentOptions _paymentOptions;

    public ApplicationService(IStringLocalizer<AccountResource> _localizer, IOptionsMonitor<PaymentOptions> paymentOptions)
    {
        this._localizer = _localizer;
        _paymentOptions = paymentOptions.CurrentValue;
    }

    public async Task<Application> CreateApplicationForPWD(User user, DistanceForPWD distance, List<string> oldStarterKidCodes)
    {
        if (user.DateOfConfirmation == null)
        {
            throw new UserAgreementLicenseAgreementException();
        }
        if (user.IsDisable != true)
        {
            throw new NotPWDException();
        }
        var marathon = distance.Marathon;
        var today = DateTime.Now.Date;
        if (today < marathon.StartDateAcceptingApplications.Date || today > marathon.EndDateAcceptingApplications.Date)
        {
            throw new OutsideRegistationDateException();
        }

        if (distance.RemainingPlaces <= 0)
            throw new NoPlacesException();

        var starterKitCode = GenerateStarterKitCode(oldStarterKidCodes);
        
        Application result = new Application()
        {
            User = user,
            UserId = user.Id,
            Date = DateTime.Now,
            Marathon = distance.Marathon,
            DistanceForPWD = distance,
            Number = distance.StartNumbersFrom + distance.RegisteredParticipants,
            StarterKit = StartKitEnum.NotIssued,
            Payment = Entities.Applications.ApplicationEnums.PaymentMethodEnum.PWD,
            StarterKitCode = starterKitCode,
        };
        distance.RegisteredParticipants += 1;


        return result;
    }


    public async Task<Application> CreateApplicationViaPromocode(User user, Distance distance, List<string> oldStarterKidCodes, Promocode promocode)
    {
        if (user.DateOfConfirmation == null)
        {
            throw new UserAgreementLicenseAgreementException();
        }
        var marathon = distance.Marathon;
        var today = DateTime.Now.Date;
        if (today < marathon.StartDateAcceptingApplications.Date || today > marathon.EndDateAcceptingApplications.Date)
        {
            throw new OutsideRegistationDateException();
        }

        var userAge = user.GetAge();
        DistanceAge selecetedDistanceAge = null;
        foreach (var distanceAge in distance.DistanceAges)
        {
            if (userAge >= distanceAge.AgeFrom && userAge <= distanceAge.AgeTo && user.Gender == distanceAge.Gender)
            {
                selecetedDistanceAge = distanceAge;
                break;
            }
        }

        if (selecetedDistanceAge == null)
            throw new NoDistanceAgeException();


        //if ((distance.RemainingPlaces - distance.Applications.Where(x => x.RemovalTime != null).Count()) <= 0)
        //    throw new NoPlacesException();


        if (promocode.Voucher.isActive == false)
        {
            throw new DeactivatedVoucherException();
        }

        if (promocode.IsActivated == true)
        {
            throw new ActivatedPromocodeException();
        }

        Application result = new Application()
        {
            User = user,
            UserId = user.Id,
            Date = DateTime.Now,
            Marathon = distance.Marathon,
            Distance = distance,
            DistanceAge = selecetedDistanceAge,
            Number = distance.StartNumbersFrom + distance.ActivatedReservedPlaces + distance.RegisteredParticipants,
            StarterKit = StartKitEnum.NotIssued,
            Payment = PaymentMethodEnum.Voucher,
            Promocode = promocode
        };
        promocode.IsActivated = true;
        promocode.User = user;
        distance.ActivatedReservedPlaces += 1;

        var starterKitCode = GenerateStarterKitCode(oldStarterKidCodes);
        result.StarterKitCode = starterKitCode;

        return result;

    }

    public string GenerateStarterKitCode(List<string> generatedPromocodes)
    {

        Random random = new Random();
        int lengthOfCode = 6;

        char[] keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890".ToCharArray();
        var result = "";
        int i = 0;
        while (i < 1)
        {
            var promocode = Enumerable
            .Range(1, lengthOfCode) // for(i.. )
            .Select(k => keys[random.Next(0, keys.Length - 1)])  // generate a new random char
            .Aggregate("", (e, c) => e + c); // join into a string
            promocode = promocode.Replace(" ", "");

            if (!generatedPromocodes.Contains(promocode))
            {
                i += 1;
                result = promocode;
            }
        }
        return result;
    }

    public Application IssueStarterKit(Application application, string? fullNameRecipient, StartKitEnum starterKit)
    {
        if (application.DateOfIssue != null)
            throw new AlreadyIssuedStarterKitException();
        if (fullNameRecipient != null)
            application.FullNameRecipient = fullNameRecipient;
        application.StarterKit = starterKit;
        application.DateOfIssue = DateTime.UtcNow;
        return application;
    }

    public async Task<string> CreatePaymentAsync(Application application)
    {
        var user = application.User;
        var distance = application.Distance;
        var today = DateTime.Now.Date;
        double priceOfDistance = 0;

        foreach (var price in distance.DistancePrices)
        {
            if (price.DateStart <= today && today <= price.DateEnd)
            {
                priceOfDistance = price.Price;
                break;
            }
        }

        string description = "Payment";
        string salt = user.Email;
        string orderId = user.Email;
        string text = _paymentOptions.InitPaymentUrl + ";" + priceOfDistance + ";" + description + ";" + _paymentOptions.MerchantId + ";" + orderId + ";" + salt + ";" + _paymentOptions.SecretKey;

        MD5 md5 = new MD5CryptoServiceProvider();
 
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

        byte[] result = md5.Hash;

        StringBuilder sig = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            sig.Append(result[i].ToString("x2"));
        }
 
        var client = new RestClient(_paymentOptions.Url);
        var request = new RestRequest(_paymentOptions.InitPaymentUrl);
        var dotNetXmlDeserializer = new DotNetXmlDeserializer();
        request.AddHeader("Content-type", "application/json");

        request.AddJsonBody(new PaymentRequest
        {
            pg_order_id = user.Email,
            pg_merchant_id = _paymentOptions.MerchantId,
            pg_amount = priceOfDistance,
            pg_description = description,
            pg_salt = salt,
            pg_sig = sig.ToString(),
        });

        var response = await client.PostAsync(request);

        var data = dotNetXmlDeserializer.Deserialize<PaymentResponse>(response);

        if (response.ErrorException != null)
            throw new PaymentNotInitializedException(response.ErrorMessage, response.StatusCode);

        return data.pg_redirect_url;
    }

    public Application CreateApplicationViaMoney(User user, Distance distance, List<string> oldStarterKidCodes)
    {
        if (user.DateOfConfirmation == null)
        {
            throw new UserAgreementLicenseAgreementException();
        }
        var marathon = distance.Marathon;
        var today = DateTime.Now.Date;
        if (today < marathon.StartDateAcceptingApplications.Date || today > marathon.EndDateAcceptingApplications.Date)
        {
            throw new OutsideRegistationDateException();
        }

        var userAge = user.GetAge();
        DistanceAge selecetedDistanceAge = null;
        foreach (var distanceAge in distance.DistanceAges)
        {
            if (userAge >= distanceAge.AgeFrom && userAge <= distanceAge.AgeTo && user.Gender == distanceAge.Gender)
            {
                selecetedDistanceAge = distanceAge;
                break;
            }
        }

        if (selecetedDistanceAge == null)
            throw new NoDistanceAgeException();

        if (distance.RemainingPlaces <= 0)
            throw new NoPlacesException();

        var starterKitCode = GenerateStarterKitCode(oldStarterKidCodes);

        Application result = new Application()
        {
            User = user,
            UserId = user.Id,
            Date = DateTime.Now,
            Marathon = distance.Marathon,
            Distance = distance,
            DistanceAge = selecetedDistanceAge,
            StarterKit = StartKitEnum.NotIssued,
            Payment = PaymentMethodEnum.Money,
            StarterKitCode = starterKitCode,
        };

        distance.RegisteredParticipants += 1;
        return result;
    }
}