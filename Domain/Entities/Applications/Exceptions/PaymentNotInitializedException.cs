using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Applications.Exceptions;

public class PaymentNotInitializedException : DomainException
{
    public PaymentNotInitializedException(string message, HttpStatusCode statusCode) : base(message, (int)statusCode)
    {
    }
}


