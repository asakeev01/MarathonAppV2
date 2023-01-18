using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class AlreadyRegisteredException : DomainException
{
    public AlreadyRegisteredException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.AlreadyRegisteredError], 2)
    {
    }
}
