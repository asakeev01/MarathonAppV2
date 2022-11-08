using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Infrastructure.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.UseCases.Marathons.Commands.PutMarathon;

public class PutMarathonCommand : IRequest<HttpStatusCode>
{
    public PutMarathonInDto MarathonDto { get; set; }
}

public class PutMarathonCommandHandler : IRequestHandler<PutMarathonCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedFileService _savedFileService;

    public PutMarathonCommandHandler(IUnitOfWork unit, ISavedFileService savedFileService)
    {
        _unit = unit;
        _savedFileService = savedFileService;
    }

    public async Task<HttpStatusCode> Handle(PutMarathonCommand cmd, CancellationToken cancellationToken)
    {
        var marathon = await _unit.MarathonRepository
            .FirstAsync(x => x.Id == cmd.MarathonDto.Id, include: source => source
            .Include(a => a.MarathonTranslations).ThenInclude(a => a.Logo)
            .Include(a => a.DistancesForPWD)
            .Include(a => a.Distances).ThenInclude(a => a.DistancePrices)
            .Include(a => a.Distances).ThenInclude(a => a.DistanceAges));

        var translations = marathon.MarathonTranslations;

        cmd.MarathonDto.Adapt(marathon);

        //To recover logos, that are deleted after adapt
        foreach (var translation in marathon.MarathonTranslations)
        {
            translation.Logo = translations.Where(x => x.Id == translation.Id).First().Logo;

        }

        await _unit.MarathonRepository.Update(marathon, save:true);
        return HttpStatusCode.OK;
    }
}
