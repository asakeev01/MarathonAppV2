using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class NoDistanceAgeException : DomainException
{
    public NoDistanceAgeException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.NoDistanceAgeError], 6)
    {
    }
}

