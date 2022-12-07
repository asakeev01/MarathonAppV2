using Domain.Common.Exceptions;

namespace Domain.Entities.Vouchers.Exceptions;

public class DeleteActivatedPromocodeException : DomainException
{
    public DeleteActivatedPromocodeException() :
        base("You cant delete activated promocode.", 10)
    {
    }
}
