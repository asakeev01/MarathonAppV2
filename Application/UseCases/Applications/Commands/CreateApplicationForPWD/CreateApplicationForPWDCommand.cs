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
    public int DistanceForPWDId { get; set; }
}

public class CreateApplicationForPWDCommandHandler : IRequestHandler<CreateApplicationForPWDCommand, int>
{
    private readonly IUnitOfWork _unit;
    private readonly IApplicationService _applicationService;
    private readonly IEmailService _emailService;
    private readonly IStringLocalizer<SharedResource> _localizer;
    private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    public CreateApplicationForPWDCommandHandler(IStringLocalizer<SharedResource> _localizer, IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService)
    {
        this._localizer = _localizer;
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
    }

    public async Task<int> Handle(CreateApplicationForPWDCommand cmd, CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync();
        try
        {
            var user = await _unit.UserRepository.FirstAsync(x => x.Id == cmd.UserId);
            var distance = await _unit.DistanceForPwdRepository.FirstAsync(x => x.Id == cmd.DistanceForPWDId, include: source => source
                .Include(a => a.Marathon)
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
            await _unit.DistanceForPwdRepository.Update(distance, save: true);
            await _emailService.SendStarterKitCodeAsync(user.Email, application.StarterKitCode);

            return application.Id;
        }
        finally
        {
            semaphore.Release();
        }
    }
}