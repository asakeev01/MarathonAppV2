using System;
using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class AlreadyIssuedStarterKitException : DomainException
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    public AlreadyIssuedStarterKitException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.AlreadyIssuedStarterKitError], 8)
    {
        this._localizer = _localizer;
    }
}

