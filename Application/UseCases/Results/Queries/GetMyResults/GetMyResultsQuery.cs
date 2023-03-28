using System;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Results.Queries.GetMyResults;

public class GetMyResultsQuery : IRequest<QueryablePaging<GetMyResultOutDto>>
{
    public int UserId { get; set; }
    public GridifyQuery Query { get; set; }
    public string LanguageCode { get; set; }
}

public class GetMyResultsHandler : IRequestHandler<GetMyResultsQuery, QueryablePaging<GetMyResultOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetMyResultsHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetMyResultOutDto>> Handle(GetMyResultsQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);

        var results = _unit.ResultRepository.FindByCondition(x => x.Application.UserId == request.UserId, include: source => source
        .Include(x => x.Application).ThenInclude(x => x.Distance)
        .Include(x => x.Application).ThenInclude(x => x.DistanceAge)
        .Include(x => x.Application).ThenInclude(x => x.User)
        .Include(x => x.Application).ThenInclude(x => x.Marathon).ThenInclude(x => x.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode)
        ));

        var result = results.Adapt<IEnumerable<GetMyResultOutDto>>().AsQueryable().GridifyQueryable(request.Query);

        return result;
    }
}
