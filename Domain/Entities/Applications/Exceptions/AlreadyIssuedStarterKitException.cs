using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class AlreadyIssuedStarterKitException : DomainException
{
    public AlreadyIssuedStarterKitException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.AlreadyIssuedStarterKitError], 1)
    {
    }
}

