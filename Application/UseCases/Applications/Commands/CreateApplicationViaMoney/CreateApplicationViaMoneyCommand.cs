using System;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Applications.Exceptions;
using Domain.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

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
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly ILogger<CreatePaymentHandler> _logger;

    public CreatePaymentHandler(IStringLocalizer<SharedResource> _localizer, IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService, IPaymentService paymentService, ILogger<CreatePaymentHandler> logger)
    {
        this._localizer = _localizer;
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
        _paymentService = paymentService;
        _logger = logger;
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
            var marathon = distance.Marathon;
            var application = await _unit.ApplicationRepository.GetFirstOrDefaultAsync(predicate: a => a.Marathon == marathon && a.User == user, include: source => source
                .Include(a => a.Distance));
            if (application != null)
            {
                if (application.Distance.Id == cmd.DistanceId)
                    return application.PaymentUrl;
                else
                {
                    //await _paymentService.SendDeletePaymentAsync(application);
                    if (application.RemovalTime == null)
                        throw new AlreadyRegisteredException(_localizer);
                    application.Distance.InitializedPlaces -= 1;
                    await _unit.DistanceRepository.Update(application.Distance, save:true);
                    _logger.LogInformation($"DistanceId = {application.Distance.Id}; InitializedPlaces -= 1");
                    await _unit.ApplicationRepository.Delete(application, save: true);
                }
            }
            var oldStarterKitCodes = _unit.ApplicationRepository.FindByCondition(x => x.MarathonId == distance.MarathonId).Select(x => x.StarterKitCode).ToList();
            application = _applicationService.CreateApplicationViaMoney(user, distance, oldStarterKitCodes);
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

