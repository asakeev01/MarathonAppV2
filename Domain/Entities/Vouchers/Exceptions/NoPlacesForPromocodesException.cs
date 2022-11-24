using Domain.Common.Exceptions;

namespace Domain.Entities.Vouchers.Exceptions;

public class NoPlacesForPromocodesException : DomainException
{
    public NoPlacesForPromocodesException(string distance) :
        base($"No places for promocodes to {distance} distance.", 4)
    {
    }
}
