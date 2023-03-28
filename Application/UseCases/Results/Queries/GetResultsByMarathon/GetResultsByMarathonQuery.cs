using System;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Results.Queries.GetResultsByMarathon;

public class GetResultsByMarathonQuery : IRequest<QueryablePaging<GetResultsByMarathonOutDto>>
{
    public int MarathonId { get; set; }
    public GridifyQuery Query { get; set; }
    public string LanguageCode { get; set; }
}

public class GetMyResultsHandler : IRequestHandler<GetResultsByMarathonQuery, QueryablePaging<GetResultsByMarathonOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetMyResultsHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetResultsByMarathonOutDto>> Handle(GetResultsByMarathonQuery request,
        CancellationToken cancellationToken)
    {
        var results = _unit.ResultRepository.FindByCondition(x => x.Application.MarathonId == request.MarathonId, include: source => source
        .Include(x => x.Application).ThenInclude(x => x.Distance)
        .Include(x => x.Application).ThenInclude(x => x.DistanceAge)
        .Include(x => x.Application).ThenInclude(x => x.User)
        .Include(x => x.Application).ThenInclude(x => x.Marathon).ThenInclude(x => x.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode)
        )); ;

        var result = results.Adapt<IEnumerable<GetResultsByMarathonOutDto>>().AsQueryable().GridifyQueryable(request.Query);

        return result;
    }
}
