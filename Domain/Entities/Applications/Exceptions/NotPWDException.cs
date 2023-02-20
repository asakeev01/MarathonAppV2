using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class NotPWDException : DomainException
{
    public NotPWDException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.NotPWDError], 8)
    {
    }
}
