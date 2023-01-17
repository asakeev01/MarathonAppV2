using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Applications.Exceptions;

public class DeactivatedVoucherException : DomainException
{
    public DeactivatedVoucherException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.DeactivatedVoucherError], 3)
    {
    }
}
