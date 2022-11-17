using Domain.Common.Exceptions;
using System.Net;

namespace Domain.Entities.Applications.Exceptions;

public class NotPWDException : DomainException
{
    public NotPWDException() : base("Your disability has not been confirmed", 7)
    {
    }
}
