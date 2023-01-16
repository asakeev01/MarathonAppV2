using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Domain.Entities.Applications.Exceptions;

public class AlreadyRegisteredException : DomainException
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    public AlreadyRegisteredException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.AlreadyRegisteredError], 8)
    {
        this._localizer = _localizer;
    }
}
