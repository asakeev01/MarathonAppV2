using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Applications.Queries.MyApplications;

public class MyApplicationsQuery : IRequest<QueryablePaging<MyApplicationsQueryOutDto>>
{
    public int UserId { get; set; }
    public string LanguageCode { get; set; }
    public GridifyQuery Query { get; set; }
}

public class MyApplicationsQueryyHandler : IRequestHandler<MyApplicationsQuery, QueryablePaging<MyApplicationsQueryOutDto>>
{
    private readonly IUnitOfWork _unit;

    public MyApplicationsQueryyHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<MyApplicationsQueryOutDto>> Handle(MyApplicationsQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);
        var applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.UserId == request.UserId && x.RemovalTime == null && x.Date.Date >= DateTime.Now.Date, include: source => source
            .Include(x => x.Distance)
            .Include(x => x.DistanceAge)
            .Include(x => x.DistanceForPWD)
            .Include(x => x.Marathon).ThenInclude(a => a.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode))
        );

        var result = applications.Adapt<IEnumerable<MyApplicationsQueryOutDto>>().AsQueryable().GridifyQueryable(request.Query);

        return result;
    }
}
