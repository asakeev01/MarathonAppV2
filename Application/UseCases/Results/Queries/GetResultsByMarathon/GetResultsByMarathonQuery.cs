using System;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Results.Queries.GetResultsByMarathon;

public class GetResultsByMarathonQuery : IRequest<GetResultsByMarathonOutDto>
{
    public int MarathonId { get; set; }
    public GridifyQuery Query { get; set; }
    public string LanguageCode { get; set; }
}

public class GetMyResultsHandler : IRequestHandler<GetResultsByMarathonQuery, GetResultsByMarathonOutDto>
{
    private readonly IUnitOfWork _unit;

    public GetMyResultsHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<GetResultsByMarathonOutDto> Handle(GetResultsByMarathonQuery request,
        CancellationToken cancellationToken)
    {
        var results = _unit.ResultRepository.FindByCondition(x => x.Application.MarathonId == request.MarathonId, include: source => source
        .Include(x => x.Application).ThenInclude(x => x.Distance)
        .Include(x => x.Application).ThenInclude(x => x.DistanceAge)
        .Include(x => x.Application).ThenInclude(x => x.User)
        .Include(x => x.Application).ThenInclude(x => x.Marathon).ThenInclude(x => x.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode)
        )); ;

        var result = results.Adapt<IEnumerable<GetResultsByMarathonOutDto.ResultsDto>>().AsQueryable().GridifyQueryable(request.Query);

        var marathon = await _unit.MarathonRepository.FirstAsync(x => x.Id == request.MarathonId, include: source => source
        .Include(x => x.Distances).ThenInclude(x => x.DistanceAges));

        var distances = marathon.Distances.Select(x => x.Name).ToList();
        var distanceAges = marathon.Distances.SelectMany(x => x.DistanceAges.Select(x => $"{x.AgeFrom}-{x.AgeTo}")).ToList();

        var response = new GetResultsByMarathonOutDto()
        {
            Results = result,
            Distances = marathon.Distances.Adapt<IEnumerable<GetResultsByMarathonOutDto.DistanceDto>>(),
        };


        return response;
    }
}
