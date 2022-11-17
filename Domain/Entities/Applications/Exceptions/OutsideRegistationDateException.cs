using Domain.Common.Exceptions;
using System.Net;

namespace Domain.Entities.Applications.Exceptions;


public class OutsideRegistationDateException : DomainException
{
    public OutsideRegistationDateException() : base("Today's date is not in the registration range.", (int)HttpStatusCode.BadRequest)
    {
    }
}
