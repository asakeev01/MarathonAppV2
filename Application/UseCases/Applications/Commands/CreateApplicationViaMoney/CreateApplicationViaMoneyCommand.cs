using System;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Applications.Commands.CreateApplicationViaMoney;

public class CreateApplicationViaMoneyCommand : IRequest<string>
{
    public int UserId { get; set; }
    public int DistanceId { get; set; }
}

public class CreatePaymentHandler : IRequestHandler<CreateApplicationViaMoneyCommand, string>
{
    private readonly IUnitOfWork _unit;
    private readonly IApplicationService _applicationService;
    private readonly IEmailService _emailService;
    private readonly IPaymentService _paymentService;

    public CreatePaymentHandler(IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService, IPaymentService paymentService)
    {
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
        _paymentService = paymentService;
    }

    public async Task<string> Handle(CreateApplicationViaMoneyCommand cmd, CancellationToken cancellationToken)
    {
        await ApplicationNumberingSemaphore.semaphore.WaitAsync();
        try
        {
            var user = await _unit.UserRepository.FirstAsync(x => x.Id == cmd.UserId);
            var distance = await _unit.DistanceRepository.FirstAsync(x => x.Id == cmd.DistanceId, include: source => source
                .Include(a => a.Marathon)
                .Include(a => a.DistanceAges)
                .Include(a => a.DistancePrices)
                .Include(a => a.Applications)
            );

            var oldStarterKitCodes = _unit.ApplicationRepository.FindByCondition(x => x.MarathonId == distance.MarathonId).Select(x => x.StarterKitCode).ToList();
            var marathon = distance.Marathon;
            var application = _applicationService.CreateApplicationViaMoney(user, distance, oldStarterKitCodes);
            await _unit.ApplicationRepository.CreateAsync(application, save: true);
            await _unit.DistanceRepository.Update(distance, save: true);
            application = await _paymentService.SendInitPaymentAsync(application);
            return application.PaymentUrl;
        }
        finally
        {
            ApplicationNumberingSemaphore.semaphore.Release();
        }
    }
}

