﻿using Domain.Common.Resources;
using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Applications.Exceptions;
using Domain.Entities.Distances;
using Domain.Entities.Users;
using Domain.Entities.Vouchers;
using Domain.Entities.Vouchers.Exceptions;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Localization;

namespace Domain.Services;

public class ApplicationService : IApplicationService
{
    private readonly IStringLocalizer<AccountResource> _localizer;

    public ApplicationService(
        IStringLocalizer<AccountResource> _localizer)
    {
        this._localizer = _localizer;
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

        var starterKitCode = await GenerateStarterKitCode(oldStarterKidCodes);
        
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


    public async Task<Application> CreateApplication(User user, Distance distance, List<string> oldStarterKidCodes, Promocode promocode = null)
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

        if (distance.RemainingPlaces <= 0 && promocode == null)
            throw new NoPlacesException();

        var result = new Application();

        if (promocode != null)
        {
            if (promocode.Voucher.isActive == false)
            {
                throw new DeactivatedVoucherException();
            }
            var voucherApplication = await VoucherApplication(user, distance, selecetedDistanceAge, promocode);
            result = voucherApplication;
        }

        var starterKitCode = await GenerateStarterKitCode(oldStarterKidCodes);
        result.StarterKitCode = starterKitCode;

        return result;

    }

    public async Task<Application> VoucherApplication(User user, Distance distance, DistanceAge distanceAge, Promocode promocode)
    {

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
            DistanceAge = distanceAge,
            Number = distance.StartNumbersFrom + distance.ActivatedReservedPlaces + distance.RegisteredParticipants,
            StarterKit = StartKitEnum.NotIssued,
            Payment = PaymentMethodEnum.Voucher,
            Promocode = promocode
        };
        promocode.IsActivated = true;
        promocode.User = user;
        distance.ActivatedReservedPlaces += 1;
        return result;
    }

    public async Task<string> GenerateStarterKitCode(List<string> generatedPromocodes)
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
}