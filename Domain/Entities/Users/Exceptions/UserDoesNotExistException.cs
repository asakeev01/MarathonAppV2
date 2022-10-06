using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class UserDoesNotExistException : DomainException
    {
        public UserDoesNotExistException() : base("User does not exist", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}

