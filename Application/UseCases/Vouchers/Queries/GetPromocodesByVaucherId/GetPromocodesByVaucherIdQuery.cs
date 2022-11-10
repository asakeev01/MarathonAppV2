using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public class GetPromocodesByVaucherIdQuery : IRequest<QueryablePaging<GetPromocodesByVaucherIdQueryOutDto>>
{
    public int VoucherId { get; set; }
    public GridifyQuery Query { get; set; }
}

public class GetVourcherHandler : IRequestHandler<GetPromocodesByVaucherIdQuery, QueryablePaging<GetPromocodesByVaucherIdQueryOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetVourcherHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetPromocodesByVaucherIdQueryOutDto>> Handle(GetPromocodesByVaucherIdQuery request,
        CancellationToken cancellationToken)
    {
        var promocodes = _unit.PromocodeRepository
            .FindByCondition(predicate:x => x.VoucherId == request.VoucherId);

        var promocodesDto = promocodes.Adapt<IEnumerable<GetPromocodesByVaucherIdQueryOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return promocodesDto;
    }
}
