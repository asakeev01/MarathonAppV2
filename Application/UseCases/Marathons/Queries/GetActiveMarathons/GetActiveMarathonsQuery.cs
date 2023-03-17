using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Marathons.Queries.GetActiveMarathons;

public class GetActiveMarathonsQuery : IRequest<QueryablePaging<GetActiveMarathonsOutDto>>
{
    public GridifyQuery Query { get; set; }
    public string LanguageCode { get; set; }
}

public class GetActiveMarathonsHandler : IRequestHandler<GetActiveMarathonsQuery, QueryablePaging<GetActiveMarathonsOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetActiveMarathonsHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetActiveMarathonsOutDto>> Handle(GetActiveMarathonsQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);



        var marathons = _unit.MarathonRepository
            .FindByCondition(predicate: x => x.StartDateAcceptingApplications <= DateTime.UtcNow.Date && x.Date >= DateTime.UtcNow.Date,
            include: source => source
            .Include(a => a.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode))
            );
        var response = marathons.Adapt<IEnumerable<GetActiveMarathonsOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return response;
    }
}
