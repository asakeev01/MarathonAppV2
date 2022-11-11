using Domain.Common.Exceptions;

namespace Domain.Entities.Vouchers.Exceptions;

public class NoPlacesException : DomainException
{
    public NoPlacesException(string distance) :
        base($"No places for promocodes to {distance} distance.", 4)
    {
    }
}
