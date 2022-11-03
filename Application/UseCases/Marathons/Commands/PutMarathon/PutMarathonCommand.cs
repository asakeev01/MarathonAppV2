using Domain.Common.Contracts;
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
            .Include(a => a.MarathonTranslations)
            .Include(a => a.DistancesForPWD)
            .Include(a => a.Distances).ThenInclude(a => a.DistancePrices)
            .Include(a => a.Distances).ThenInclude(a => a.DistanceAges));
                
        var entity = cmd.MarathonDto.Adapt(marathon);
        foreach (var translation in entity.MarathonTranslations)
        {
            if (translation.Logo == null)
            {
                var logo = await _savedFileService.EmptyFile();
                translation.Logo = logo;
            }

        }
        await _unit.MarathonRepository.SaveAsync();

        return HttpStatusCode.OK;
    }
}
