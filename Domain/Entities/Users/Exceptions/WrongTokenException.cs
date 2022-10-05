using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class WrongTokenException : DomainException
    {
        public WrongTokenException() : base("Wrong token", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}

