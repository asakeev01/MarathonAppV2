using Domain.Common.Exceptions;
using System.Net;

namespace Domain.Entities.Applications.Exceptions;

public class AlreadyRegisteredException : DomainException
{
    public AlreadyRegisteredException() : base("You are already registered to this marathon.",8)
    {
    }
}
