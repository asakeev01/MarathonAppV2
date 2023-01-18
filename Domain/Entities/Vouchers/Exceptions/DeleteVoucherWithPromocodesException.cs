using Domain.Common.Exceptions;

namespace Domain.Entities.Vouchers.Exceptions;

public class DeleteVoucherWithPromocodesException : DomainException
{
    public DeleteVoucherWithPromocodesException() :
        base("You cant delete voucher with promocodes.", 10)
    {
    }
}
