using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Applications.Exceptions;

public class SignaturesDoNotMatchException : DomainException
{
    public SignaturesDoNotMatchException() : base("There is no places to this distance.", (int)HttpStatusCode.BadRequest)
    {
    }
}

