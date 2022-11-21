using Core.UseCases.Marathons.Commands.CreateMarathon;
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

namespace Core.UseCases.Applications.Commands.CraeteApplication;

public class CreateApplicationCommand : IRequest<int>
{
    public int UserId { get; set; }
    public int DistanceId { get; set; }
    public string? Promocode { get; set; }
}

public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, int>
{
    private readonly IUnitOfWork _unit;
    private readonly IApplicationService _applicationService;

    public CreateApplicationCommandHandler(IUnitOfWork unit, IApplicationService applicationService)
    {
        _unit = unit;
        _applicationService = applicationService;
    }

    public async Task<int> Handle(CreateApplicationCommand cmd, CancellationToken cancellationToken)
    {
        var user = await _unit.UserRepository.FirstAsync(x => x.Id == cmd.UserId);
        var distance = await _unit.DistanceRepository.FirstAsync(x => x.Id == cmd.DistanceId, include: source => source
            .Include(a => a.Marathon)
            .Include(a => a.DistanceAges)
            .Include(a => a.DistancePrices)
            .Include(a => a.Applications)
        );

        //var old_applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.User == user && x.Marathon == distance.Marathon).ToList();

        //if (old_applications.Count != 0)
        //{
        //    throw new AlreadyRegisteredException();
        //}


        var marathon = distance.Marathon;
        

        if (cmd.Promocode != null)
        {
            var promocode = await _unit.PromocodeRepository.FirstAsync(x => x.Code == cmd.Promocode && x.Distance == distance);
            var application = await _applicationService.CreateApplication(user, distance, promocode);
            await _unit.ApplicationRepository.CreateAsync(application, save: true);
            await _unit.PromocodeRepository.Update(promocode, save: true);
            await _unit.DistanceRepository.Update(distance, save: true);
            return application.Id;
        }

        return -1;
    }
}