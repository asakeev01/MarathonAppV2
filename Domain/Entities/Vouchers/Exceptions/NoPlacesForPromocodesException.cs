using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Vouchers.Exceptions;

public class NoPlacesForPromocodesException : DomainException
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    public NoPlacesForPromocodesException(IStringLocalizer<SharedResource> _localizer, string distance) : base(_localizer[SharedResource.NoPlacesForPromocodesError, distance], 22)
    {
    }
}
