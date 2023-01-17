using Core.Common.Helpers;
using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Applications.Exceptions;
using Domain.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Core.UseCases.Applications.Commands.CreateApplicationViaPromocode;

public class CreateApplicationViaPromocodeCommand : IRequest<int>
{
    public int UserId { get; set; }
    public int DistanceId { get; set; }
    public string? Promocode { get; set; }
}

public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationViaPromocodeCommand, int>
{
    private readonly IUnitOfWork _unit;
    private readonly IApplicationService _applicationService;
    private readonly IEmailService _emailService;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public CreateApplicationCommandHandler(IStringLocalizer<SharedResource> _localizer, IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService)
    {
        this._localizer = _localizer;
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
    }

    public async Task<int> Handle(CreateApplicationViaPromocodeCommand cmd, CancellationToken cancellationToken)
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

            var old_applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.User == user && x.Marathon == distance.Marathon).ToList();

            if (old_applications.Count != 0)
            {
                throw new AlreadyRegisteredException(_localizer);
            }

            var marathon = distance.Marathon;

            var oldStarterKitCodes = _unit.ApplicationRepository.FindByCondition(x => x.MarathonId == distance.MarathonId).Select(x => x.StarterKitCode).ToList();

            var promocode = await _unit.PromocodeRepository.FirstAsync(x => x.Code == cmd.Promocode && x.Distance == distance, include: source => source.Include(x => x.Voucher));

            var application = await _applicationService.CreateApplicationViaPromocode(user, distance, oldStarterKitCodes, promocode);
            await _unit.ApplicationRepository.CreateAsync(application, save: true);
            await _unit.PromocodeRepository.Update(promocode, save: true);
            await _unit.DistanceRepository.Update(distance, save: true);
            await _emailService.SendStarterKitCodeAsync(user.Email, application.StarterKitCode);
            
            return application.Id;
        }
        finally
        {
            ApplicationNumberingSemaphore.semaphore.Release();
        }
    }
}