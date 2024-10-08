﻿using System.Security.Cryptography;
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
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Domain.Services;

public class ApplicationService : IApplicationService
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly ILogger<ApplicationService> _logger;

    public ApplicationService(IStringLocalizer<SharedResource> _localizer, ILogger<ApplicationService> logger)
    {
        this._localizer = _localizer;
        _logger = logger;

    }

    public async Task<Application> CreateApplicationForPWD(User user, Distance distance, List<string> oldStarterKidCodes)
    {
        if (user.DateOfConfirmation == null)
        {
            throw new UserAgreementLicenseAgreementException(_localizer);
        }
        if (user.IsDisable != true)
        {
            throw new NotPWDException(_localizer);
        }
        var marathon = distance.Marathon;
        var today = DateTime.Now.Date;
        if (today < marathon.StartDateAcceptingApplications.Date || today > marathon.EndDateAcceptingApplications.Date)
        {
            throw new OutsideRegistationDateException(_localizer);
        }

        if (distance.RemainingPlaces <= 0)
            throw new NoPlacesException(_localizer);

        var starterKitCode = GenerateStarterKitCode(oldStarterKidCodes);
        decimal priceOfDistance = 0;
        foreach (var price in distance.DistancePrices)
        {
            if (price.DateStart <= today && today <= price.DateEnd)
            {
                priceOfDistance = price.Price;
                break;
            }
        }

        Application result = new Application()
        {
            User = user,
            UserId = user.Id,
            Date = DateTime.Now,
            Marathon = distance.Marathon,
            Distance = distance,
            Number = distance.StartNumbersFrom + distance.ActivatedReservedPlaces + distance.RegisteredParticipants,
            StarterKit = StartKitEnum.NotIssued,
            Payment = Entities.Applications.ApplicationEnums.PaymentMethodEnum.PWD,
            StarterKitCode = starterKitCode,
            IsPWD = true,
            Price = priceOfDistance,
            Paid = 0,
        };
        distance.RegisteredParticipants += 1;


        return result;
    }


    public async Task<Application> CreateApplicationViaPromocode(User user, Distance distance, List<string> oldStarterKidCodes, Promocode promocode)
    {
        if (user.DateOfConfirmation == null)
        {
            throw new UserAgreementLicenseAgreementException(_localizer);
        }
        var marathon = distance.Marathon;
        var today = DateTime.Now.Date;
        if (today < marathon.StartDateAcceptingApplications.Date || today > marathon.EndDateAcceptingApplications.Date)
        {
            throw new OutsideRegistationDateException(_localizer);
        }

        var userAge = user.GetAge(distance.Marathon.Date);
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
            throw new NoDistanceAgeException(_localizer);


        //if ((distance.RemainingPlaces - distance.Applications.Where(x => x.RemovalTime != null).Count()) <= 0)
        //    throw new NoPlacesException();


        if (promocode.Voucher.isActive == false)
        {
            throw new DeactivatedVoucherException(_localizer);
        }

        if (promocode.IsActivated == true)
        {
            throw new ActivatedPromocodeException(_localizer);
        }
        decimal priceOfDistance = 0;

        foreach (var price in distance.DistancePrices)
        {
            if (price.DateStart <= today && today <= price.DateEnd)
            {
                priceOfDistance = price.Price;
                break;
            }
        }

        Application result = new Application()
        {
            User = user,
            UserId = user.Id,
            Date = DateTime.Now,
            Marathon = distance.Marathon,
            Distance = distance,
            DistanceAge = selecetedDistanceAge,
            StarterKit = StartKitEnum.NotIssued,
            Number = distance.StartNumbersFrom + distance.ActivatedReservedPlaces + distance.RegisteredParticipants,
            Payment = PaymentMethodEnum.Voucher,
            Promocode = promocode,
            Price = priceOfDistance,
            Paid = 0
        };
        promocode.IsActivated = true;
        promocode.User = user;
        distance.ActivatedReservedPlaces += 1;

        var starterKitCode = GenerateStarterKitCode(oldStarterKidCodes);

        result.StarterKitCode = starterKitCode;

        return result;

    }

    public Application CreateApplicationViaMoney(User user, Distance distance, List<string> oldStarterKidCodes)
    {
        if (user.DateOfConfirmation == null)
        {
            throw new UserAgreementLicenseAgreementException(_localizer);
        }
        var marathon = distance.Marathon;
        var today = DateTime.Now.Date;
        if (today < marathon.StartDateAcceptingApplications.Date || today > marathon.EndDateAcceptingApplications.Date)
        {
            throw new OutsideRegistationDateException(_localizer);
        }

        var userAge = user.GetAge(distance.Marathon.Date);
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
            throw new NoDistanceAgeException(_localizer);

        if (distance.RemainingPlaces <= 0)
            throw new NoPlacesException(_localizer);

        decimal priceOfDistance = 0;

        foreach (var price in distance.DistancePrices)
        {
            if (price.DateStart <= today && today <= price.DateEnd)
            {
                priceOfDistance = price.Price;
                break;
            }
        }

        var starterKitCode = GenerateStarterKitCode(oldStarterKidCodes);

        Application result = new Application()
        {
            User = user,
            UserId = user.Id,
            Date = DateTime.Now,
            Marathon = distance.Marathon,
            Distance = distance,
            DistanceAge = selecetedDistanceAge,
            Price = priceOfDistance,
            Paid = priceOfDistance,
            StarterKit = StartKitEnum.NotIssued,
            Payment = PaymentMethodEnum.Money,
            RemovalTime = DateTime.Now.AddMinutes(65),
            StarterKitCode = starterKitCode,
        };

        distance.InitializedPlaces += 1;
        _logger.LogInformation($"DistanceId = {distance.Id}; InitializedPlaces += 1");

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
            throw new AlreadyIssuedStarterKitException(_localizer);
        if (fullNameRecipient != null)
            application.FullNameRecipient = fullNameRecipient;
        application.StarterKit = starterKit;
        application.DateOfIssue = DateTime.UtcNow;
        return application;
    }

    public Application AssignNumber(Application application, Distance distance)
    {
        var result = application;
        result.Number = distance.StartNumbersFrom + distance.ActivatedReservedPlaces + distance.RegisteredParticipants;
        result.RemovalTime = null;
        distance.InitializedPlaces -= 1;
        _logger.LogInformation($"DistanceId = {distance.Id}; InitializedPlaces -= 1");
        distance.RegisteredParticipants += 1;
        return result;
    }
}