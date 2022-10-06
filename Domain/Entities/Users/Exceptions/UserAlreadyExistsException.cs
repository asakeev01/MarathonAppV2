using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class UserAlreadyExistsException : DomainException
    {
        public UserAlreadyExistsException() : base("User already exist", (int) HttpStatusCode.BadRequest)
        {
        }
    }
}
