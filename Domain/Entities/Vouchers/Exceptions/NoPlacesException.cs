using Domain.Common.Exceptions;

namespace Domain.Entities.Vouchers.Exceptions;

public class NoPlacesException : DomainException
{
    public NoPlacesException() :
        base("No places for promocodes to this distance", 4)
    {
    }
}
