using Core.Common.Helpers;
using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Applications.Exceptions;
using Domain.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Data;

namespace Core.UseCases.Applications.Commands.CreateApplicationForPWD;

public class CreateApplicationForPWDCommand : IRequest<int>
{
    public int UserId { get; set; }
    public int DistanceId { get; set; }
}

public class CreateApplicationForPWDCommandHandler : IRequestHandler<CreateApplicationForPWDCommand, int>
{
    private readonly IUnitOfWork _unit;
    private readonly IApplicationService _applicationService;
    private readonly IEmailService _emailService;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public CreateApplicationForPWDCommandHandler(IStringLocalizer<SharedResource> _localizer, IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService)
    {
        this._localizer = _localizer;
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
    }

    public async Task<int> Handle(CreateApplicationForPWDCommand cmd, CancellationToken cancellationToken)
    {
        await ApplicationNumberingSemaphore.semaphore.WaitAsync();
        try
        {
            var user = await _unit.UserRepository.FirstAsync(x => x.Id == cmd.UserId);
            var distance = await _unit.DistanceRepository.FirstAsync(x => x.Id == cmd.DistanceId, include: source => source
                .Include(a => a.Marathon).ThenInclude(x => x.MarathonTranslations)
                .Include(a => a.DistancePrices)
            );

            var old_applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.User == user && x.Marathon == distance.Marathon).ToList();

            if (old_applications.Count != 0)
            {
                throw new AlreadyRegisteredException(_localizer);
            }
            var oldStarterKitCodes = _unit.ApplicationRepository.FindByCondition(x => x.MarathonId == distance.MarathonId).Select(x => x.StarterKitCode).ToList();
            var marathon = distance.Marathon;
            var application = await _applicationService.CreateApplicationForPWD(user, distance, oldStarterKitCodes);
            await _unit.ApplicationRepository.CreateAsync(application, save: true);
            await _unit.DistanceRepository.Update(distance, save: true);
            await _emailService.SendStarterKitCodeAsync(user.Email, application.StarterKitCode, user.Name, user.Surname, distance.Name, marathon.Date.ToString("dd/MM/yyyy"), $"ЛОВЗ(PWD)", marathon.MarathonTranslations.Where(x => x.LanguageId == 1).First().Name, marathon.MarathonTranslations.Where(x => x.LanguageId == 2).First().Name, marathon.MarathonTranslations.Where(x => x.LanguageId == 3).First().Name, application.Number.ToString());

            return application.Id;
        }
        finally
        {
            ApplicationNumberingSemaphore.semaphore.Release();
        }
    }
}