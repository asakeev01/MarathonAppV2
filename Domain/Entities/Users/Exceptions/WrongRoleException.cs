using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class WrongRoleException : DomainException
    {
        public WrongRoleException() : base("Wrong role", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}

