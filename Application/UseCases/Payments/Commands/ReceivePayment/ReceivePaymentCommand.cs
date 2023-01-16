using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using Domain.Services.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Payments.Commands.ReceivePayment;

public class ReceivePaymentCommand : IRequest<HttpStatusCode>
{
    public ReceivePaymentInDto PaymentDto { get; set; }
}

public class ReceivePaymentHandler : IRequestHandler<ReceivePaymentCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IApplicationService _applicationService;
    private readonly IEmailService _emailService;
    private readonly IPaymentService _paymentService;
    static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    public ReceivePaymentHandler(IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService, IPaymentService paymentService)
    {
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
        _paymentService = paymentService;
    }

    public async Task<HttpStatusCode> Handle(ReceivePaymentCommand cmd, CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync();
        try
        {
            var paymentRequest = cmd.PaymentDto.Adapt<ReceivePaymentDto>();
            _paymentService.IsSignatureTrue(paymentRequest);
            var application = await _unit.ApplicationRepository.FirstAsync(x => x.Id.ToString() == cmd.PaymentDto.pg_order_id, include: source => source
                .Include(a => a.Distance));
            await _unit.ApplicationRepository.CreateAsync(application, save: true);
            await _unit.DistanceRepository.Update(distance, save: true);
            application = await _paymentService.SendInitPaymentAsync(application);
            await _unit.ApplicationRepository.Update(application, save: true);
            return HttpStatusCode.OK;
        }
        finally
        {
            semaphore.Release();
        }
    }
}

