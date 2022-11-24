using Domain.Common.Exceptions;

namespace Domain.Entities.Vouchers.Exceptions;

public class ActivatedPromocodeException : DomainException
{
    public ActivatedPromocodeException() :
        base("Promocode already activated", 5)
    {
    }
}
