using System;
using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class PaymentNotDeletedException : DomainException
{
    public PaymentNotDeletedException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.OutsideRegistationDateError], 9)
    {
    }
}

