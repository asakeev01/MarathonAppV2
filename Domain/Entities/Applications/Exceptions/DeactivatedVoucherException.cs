using Domain.Common.Exceptions;

namespace Domain.Entities.Applications.Exceptions;

public class DeactivatedVoucherException : DomainException
{
    public DeactivatedVoucherException() : base("Voucher deactivated.", 9)
    {
    }
}
