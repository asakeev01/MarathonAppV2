using Domain.Common.Exceptions;
using Domain.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Domain.Entities.Vouchers.Exceptions;

public class DeleteActivatedPromocodeException : DomainException
{
    public DeleteActivatedPromocodeException(IStringLocalizer<SharedResource> _localizer) : base(_localizer[SharedResource.DeleteActivatedPromocodeError], 21)
    {
    }
}
