using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class NoPlacesException : DomainException
{
    public NoPlacesException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.NoPlacesError], 7)
    {
    }
}
