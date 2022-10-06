using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class EmailAlreadyConfirmedException : DomainException
    {
        public EmailAlreadyConfirmedException() : base("Email already confirmed", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}

