using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Marathons.Queries.GetMarathons;

public class GetMarathonsQuery : IRequest<QueryablePaging<GetMarathonsOutDto>>
{
    public GridifyQuery Query { get; set; }

    public string LanguageCode { get; set; }
}

public class GetMarathonsHandler : IRequestHandler<GetMarathonsQuery, QueryablePaging<GetMarathonsOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetMarathonsHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetMarathonsOutDto>> Handle(GetMarathonsQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);
        var marathons = (await _unit.MarathonRepository
            .GetAllAsync(include: source => source.Include(a => a.Logo).Include(a => a.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode))));
        var response = marathons.Adapt<IEnumerable<GetMarathonsOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return response;
    }
}