using System;
using System.Net;
using Core.Common.Helpers;
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

    public ReceivePaymentHandler(IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService, IPaymentService paymentService)
    {
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
        _paymentService = paymentService;
    }

    public async Task<HttpStatusCode> Handle(ReceivePaymentCommand cmd, CancellationToken cancellationToken)
    {
        await ApplicationNumberingSemaphore.semaphore.WaitAsync();
        try
        {
            var paymentRequest = cmd.PaymentDto.Adapt<ReceivePaymentDto>();
            if (paymentRequest.pg_result == 0)
                Console.WriteLine(paymentRequest.pg_result);
                return HttpStatusCode.OK;
            bool isRight = _paymentService.IsSignatureRight(paymentRequest);
            if (isRight == false)
                Console.WriteLine(isRight);
                return HttpStatusCode.OK;
            var application = await _unit.ApplicationRepository.FirstAsync(x => x.Id.ToString() == cmd.PaymentDto.pg_order_id, include: source => source
                .Include(a => a.Distance));
            var distance = application.Distance;
            var user_email = cmd.PaymentDto.pg_user_contact_email;
            application = _applicationService.AssignNumber(application, distance);
            await _unit.ApplicationRepository.Update(application, save: true);
            await _unit.DistanceRepository.Update(distance, save: true);
            await _emailService.SendStarterKitCodeAsync(user_email, application.StarterKitCode);
            return HttpStatusCode.OK;
        }
        finally
        {
            ApplicationNumberingSemaphore.semaphore.Release();
        }
    }
}

