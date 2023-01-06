using System;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Applications.Commands.CreatePayment;

public class CreatePaymentCommand : IRequest<string>
{
    public int UserId { get; set; }
    public int DistanceId { get; set; }
}

public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, string>
{
    private readonly IUnitOfWork _unit;
    private readonly IApplicationService _applicationService;
    private readonly IEmailService _emailService;

    public CreatePaymentHandler(IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService)
    {
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
    }

    public async Task<string> Handle(CreatePaymentCommand cmd, CancellationToken cancellationToken)
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
        var url = await _applicationService.CreatePaymentAsync(application);
        return url;
    }
}

