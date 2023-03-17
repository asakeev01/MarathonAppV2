using Core.Common.Helpers;
using Core.UseCases.Applications.Queries.ApplicationByMarathonPublic;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Applications.Queries.ApplicationByMarathonPublic;

public class GetApplicationByMarathonPublicQuery : IRequest<QueryablePaging<GetApplicationByMarathonPublicOutDto>>
{
    public int MarathonId { get; set; }
    public GridifyQuery Query { get; set; }
}

public class GetApplicationByMarathonPublicHandler : IRequestHandler<GetApplicationByMarathonPublicQuery, QueryablePaging<GetApplicationByMarathonPublicOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetApplicationByMarathonPublicHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetApplicationByMarathonPublicOutDto>> Handle(GetApplicationByMarathonPublicQuery request,
        CancellationToken cancellationToken)
    {
        var applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.MarathonId == request.MarathonId && x.RemovalTime == null, include: source => source
            .Include(x => x.User)
            .Include(x => x.Distance)
            .Include(x => x.DistanceAge)
            .Include(x => x.Marathon)
        );

        var result = applications.Adapt<IEnumerable<GetApplicationByMarathonPublicOutDto>>().AsQueryable().GridifyQueryable(request.Query);

        return result;
    }
}
