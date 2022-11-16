using Domain.Common.Resources;
using Domain.Entities.Applications;
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

    public async Task<Application> CreateApplicationForPWD(User user, DistanceForPWD distance)
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
        var today = DateTime.Now;
        if (today < marathon.StartDateAcceptingApplications || today > marathon.EndDateAcceptingApplications)
        {
            throw new OutsideRegistationDateException();
        }

        if (distance.RemainingPlaces <= 0)
            throw new NoPlacesException();

        Application result = new Application()
        {
            User = user,
            UserId = user.Id,
            Date = DateTime.Now,
            Marathon = distance.Marathon,
            DistanceForPWD = distance,
            Number = distance.StartNumbersFrom + distance.RegisteredParticipants,
            StarterKit = false,
            Payment = Entities.Applications.ApplicationEnums.PaymentMethodEnum.PWD,
        };
        distance.RegisteredParticipants += 1;
        return result;
    }


    public async Task<Application> CreateApplication(User user, Distance distance, Promocode promocode=null)
    {
        if(user.DateOfConfirmation == null)
        {
            throw new UserAgreementLicenseAgreementException();
        }
        var marathon = distance.Marathon;
        var today = DateTime.Now;
        if (today < marathon.StartDateAcceptingApplications || today > marathon.EndDateAcceptingApplications)
        {
            throw new OutsideRegistationDateException();
        }

        var userAge = today.Year - user.DateOfBirth.Value.Year;
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

        if (promocode != null)
        {
            return await VoucherApplication(user, distance, selecetedDistanceAge, promocode);
        }
        return new Application();

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
            Number = distance.StartNumbersFrom  + distance.ActivatedReservedPlaces + distance.RegisteredParticipants,
            StarterKit = false,
            Payment = Entities.Applications.ApplicationEnums.PaymentMethodEnum.Voucher,
            Promocode = promocode
        };
        promocode.IsActivated = true;
        promocode.User = user;
        distance.ActivatedReservedPlaces += 1;
        return result;
    }

}