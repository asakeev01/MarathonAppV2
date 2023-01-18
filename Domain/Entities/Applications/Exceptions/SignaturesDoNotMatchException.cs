using System;
using System.Net;
using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class SignaturesDoNotMatchException : DomainException
{
    public SignaturesDoNotMatchException() : base(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.OutsideRegistationDateError], 9)
    {
    }
}

