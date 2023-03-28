using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class NotFoundApplicationException : DomainException
{
    public NotFoundApplicationException(IStringLocalizer<SharedResource> _localizer, string number) : base(_localizer[SharedResource.NotFoundApplicationError, number], 31)
    {
    }
}
