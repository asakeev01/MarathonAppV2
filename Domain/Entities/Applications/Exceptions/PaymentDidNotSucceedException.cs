using System;
using System.Net;
using Domain.Common.Exceptions;

namespace Domain.Entities.Applications.Exceptions;

public class PaymentDidNotSucceedException : DomainException
{
    public PaymentDidNotSucceedException() : base("Payment did not succeed", (int)HttpStatusCode.BadRequest)
    {
    }
}

