using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class PasswordsDoNotMatchException : DomainException
    {
        public PasswordsDoNotMatchException() : base("Passwords do not match", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}

