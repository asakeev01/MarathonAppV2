using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Vouchers.Exceptions;

public class DeleteVoucherWithPromocodesException : DomainException
{
    public DeleteVoucherWithPromocodesException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.DeleteVoucherWithPromocodesError], 24)
    {
    }
}
