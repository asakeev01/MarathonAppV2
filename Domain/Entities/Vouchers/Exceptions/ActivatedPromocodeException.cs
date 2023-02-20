using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Vouchers.Exceptions;

public class ActivatedPromocodeException : DomainException
{
    public ActivatedPromocodeException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.ActivatedPromocodeError], 20)
    {
    }
}
