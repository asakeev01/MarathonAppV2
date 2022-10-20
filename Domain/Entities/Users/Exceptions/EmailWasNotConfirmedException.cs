using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class EmailWasNotConfirmedException : DomainException
    {
        public EmailWasNotConfirmedException() : base("Email was not confirmed.", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}

