using System;
using System.Net;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Domain.Entities.Users;
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
            {
                Console.WriteLine(paymentRequest.pg_result);
                return HttpStatusCode.OK;
            }
            bool isRight = _paymentService.IsSignatureRight(paymentRequest);
            if (isRight == false)
            {
                return HttpStatusCode.OK;
            }
            var application = await _unit.ApplicationRepository.FirstAsync(x => x.Id.ToString() == cmd.PaymentDto.pg_order_id, include: source => source
                .Include(a => a.Distance)
                .Include(a => a.DistanceAge)
                .Include(a => a.User)
                .Include(a => a.Marathon).ThenInclude(x => x.MarathonTranslations));
            var distance = application.Distance;
            var user_email = cmd.PaymentDto.pg_user_contact_email;
            application = _applicationService.AssignNumber(application, distance);
            await _unit.ApplicationRepository.Update(application, save: true);
            await _unit.DistanceRepository.Update(distance, save: true);

            var user = application.User;
            var marathon = application.Marathon;
            await _emailService.SendStarterKitCodeAsync(user.Email, application.StarterKitCode, user.Name, user.Surname, distance.Name, marathon.Date.ToString("dd/MM/yyyy"), $"{application.DistanceAge.AgeFrom}-{application.DistanceAge.AgeTo}", marathon.MarathonTranslations.Where(x => x.LanguageId == 1).First().Name, marathon.MarathonTranslations.Where(x => x.LanguageId == 2).First().Name, marathon.MarathonTranslations.Where(x => x.LanguageId == 3).First().Name, application.Number.ToString());
            return HttpStatusCode.OK;
        }
        finally
        {
            ApplicationNumberingSemaphore.semaphore.Release();
        }
    }
}

