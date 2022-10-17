using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class InvalidTokenException : DomainException
    {
        public InvalidTokenException() : base("Invalid token", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}

