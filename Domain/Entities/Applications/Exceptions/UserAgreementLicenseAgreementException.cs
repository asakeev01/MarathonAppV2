using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class UserAgreementLicenseAgreementException : DomainException
{
    public UserAgreementLicenseAgreementException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.UserAgreementLicenseAgreementError], 10)
    {
    }
}
