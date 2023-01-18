
using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Vouchers.Exceptions;

public class InvalidPromocodeException : DomainException
{
    public InvalidPromocodeException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.InvalidPromocodeError], 23)
    {
    }
}
