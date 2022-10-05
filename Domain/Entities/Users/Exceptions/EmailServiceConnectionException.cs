using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Users.Exceptions
{
    public class EmailServiceConnectionException : DomainException
    {
        public EmailServiceConnectionException() : base("Unable to connect email service", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}

