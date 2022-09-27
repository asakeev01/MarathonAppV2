using Core.Common.Helpers;
using Core.UseCases.Marathons.Queries.GetMarathons;
using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Marathons.Queries.GetMarathon;

public class GetMarathonQuery : IRequest<GetMarathonOutDto>
{
    public string LanguageCode { get; set; }
    public int MarathonId { get; set; }
}

public class GetMarathonHandler : IRequestHandler<GetMarathonQuery, GetMarathonOutDto>
{
    private readonly IMarathonRepository _marathonRepository;
    private readonly IDistanceRepository _distanceRepository;

    public GetMarathonHandler(IMarathonRepository marathonRepository, IDistanceRepository distanceRepository)
    {
        _marathonRepository = marathonRepository;
        _distanceRepository = distanceRepository;
    }

    public async Task<GetMarathonOutDto> Handle(GetMarathonQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);
        var marathon = await _marathonRepository
            .FirstAsync(x => x.Id == request.MarathonId, include: source => source.Include(a => a.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode)));
        var marathonDto = marathon.Adapt<GetMarathonOutDto>();
        var distances =  _distanceRepository
            .FindByCondition(x => x.MarathonId == request.MarathonId, include: source => source.Include(a => a.DistanceCategory.DistanceCategoryTranslations.Where(t => t.Language.Code == request.LanguageCode)).Include(a=> a.DistanceAges).Include(a=>a.DistancePrices));
        marathonDto.Distances = distances.Adapt<IEnumerable<GetMarathonOutDto.DistanceDto>>();
        return marathonDto;
    }
}