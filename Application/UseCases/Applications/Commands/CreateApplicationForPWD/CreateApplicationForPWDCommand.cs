﻿using Core.UseCases.Marathons.Commands.CreateMarathon;
using Domain.Common.Contracts;
using Domain.Entities.Applications.Exceptions;
using Domain.Entities.Marathons;
using Domain.Entities.Users;
using Domain.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

    public CreateApplicationForPWDCommandHandler(IUnitOfWork unit, IApplicationService applicationService, IEmailService emailService)
    {
        _unit = unit;
        _applicationService = applicationService;
        _emailService = emailService;
    }

    public async Task<int> Handle(CreateApplicationForPWDCommand cmd, CancellationToken cancellationToken)
    {
        var user = await _unit.UserRepository.FirstAsync(x => x.Id == cmd.UserId);
        var distance = await _unit.DistanceForPwdRepository.FirstAsync(x => x.Id == cmd.DistanceForPWDId, include: source => source
            .Include(a => a.Marathon)
        );

        //var old_applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.User == user && x.Marathon == distance.Marathon).ToList();

        //if (old_applications.Count != 0)
        //{
        //    throw new AlreadyRegisteredException();
        //}

        var oldStarterKitCodes = _unit.ApplicationRepository.FindByCondition(x => x.MarathonId == distance.MarathonId).Select(x => x.StarterKitCode).ToList();

        var marathon = distance.Marathon;

        var application = await _applicationService.CreateApplicationForPWD(user, distance, oldStarterKitCodes);
        await _unit.ApplicationRepository.CreateAsync(application, save: true);
        await _unit.DistanceForPwdRepository.Update(distance, save: true);
        await _emailService.SendStarterKitCodeAsync(user.Email, application.StarterKitCode);
        return application.Id;
    }
}