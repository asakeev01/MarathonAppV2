using System;
using System.Net;
using Domain.Entities.Applications;
using Domain.Services.Models;

namespace Domain.Common.Contracts;

public interface IPaymentService
{
    Task<Application> SendInitPaymentAsync(Application application);
    Task<HttpStatusCode> SendDeletePaymentAsync(Application application);
    bool IsSignatureRight(ReceivePaymentDto receivePaymentDto);
    PaymentResponse CreateResponseSignature(PaymentResponse paymentResponse);
}

