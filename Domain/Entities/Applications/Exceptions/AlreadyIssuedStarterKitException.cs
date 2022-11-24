using System;
using Domain.Common.Exceptions;

namespace Domain.Entities.Applications.Exceptions;

public class AlreadyIssuedStarterKitException : DomainException
{
    public AlreadyIssuedStarterKitException() : base("Starter kit is already issued.", 8)
    {
    }
}

