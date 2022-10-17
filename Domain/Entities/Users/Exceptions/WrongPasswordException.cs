using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class WrongPasswordException : DomainException
    {
        public WrongPasswordException() : base("Wrong password", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}

