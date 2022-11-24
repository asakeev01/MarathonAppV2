using Domain.Common.Exceptions;
using System.Net;

namespace Domain.Entities.Applications.Exceptions;

public class NoPlacesException : DomainException
{
    public NoPlacesException() : base("There is no places to this distance.", (int)HttpStatusCode.BadRequest)
    {
    }
}
