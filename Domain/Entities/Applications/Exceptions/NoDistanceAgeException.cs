using Domain.Common.Exceptions;
using System.Net;

namespace Domain.Entities.Applications.Exceptions;

public class NoDistanceAgeException : DomainException
{
    public NoDistanceAgeException() : base("There is no distance age category for your age and gender.", (int)HttpStatusCode.BadRequest)
    {
    }
}
