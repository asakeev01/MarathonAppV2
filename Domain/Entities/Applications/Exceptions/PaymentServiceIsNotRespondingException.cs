using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Applications.Exceptions;

public class PaymentServiceIsNotRespondingException : DomainException
{
    public PaymentServiceIsNotRespondingException(string message, int statusCode) : base(message, statusCode)
    {
    }
}

