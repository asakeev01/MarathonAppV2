using System;
using System.Net;
using Domain.Entities.Applications;
using Domain.Services.Models;

namespace Domain.Common.Contracts;

public interface IPaymentService
{
    Task<Application> SendInitPaymentAsync(Application application);
    HttpStatusCode IsSignatureTrue(ReceivePaymentDto receivePaymentDto);
}

