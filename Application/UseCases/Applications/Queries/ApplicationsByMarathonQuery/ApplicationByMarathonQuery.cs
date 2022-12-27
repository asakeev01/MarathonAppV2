using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Applications.Queries.ApplicationsByMarathonQuery;

public class ApplicationByMarathonQuery : IRequest<QueryablePaging<ApplicationByMarathonQueryOutDto>>
{
    public int MarathonId { get; set; }
    public GridifyQuery Query { get; set; }
}

public class ApplicationByMarathonQueryHandler : IRequestHandler<ApplicationByMarathonQuery,QueryablePaging<ApplicationByMarathonQueryOutDto>>
{
    private readonly IUnitOfWork _unit;

    public ApplicationByMarathonQueryHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<ApplicationByMarathonQueryOutDto>> Handle(ApplicationByMarathonQuery request,
        CancellationToken cancellationToken)
    {
        var applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.MarathonId == request.MarathonId, include: source => source
            .Include(x => x.User).ThenInclude(x => x.Status)
            .Include(x => x.Promocode).ThenInclude(x => x.Voucher)
        );

        var result = applications.Adapt<IEnumerable<ApplicationByMarathonQueryOutDto>>().AsQueryable().GridifyQueryable(request.Query);

        return result;
    }
}
