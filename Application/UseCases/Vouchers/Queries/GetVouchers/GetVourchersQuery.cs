using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public class GetVourchersQuery : IRequest<QueryablePaging<GetVourchersQueryOutDto>>
{
    public GridifyQuery Query { get; set; }
}

public class GetVourchersHandler : IRequestHandler<GetVourchersQuery, QueryablePaging<GetVourchersQueryOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetVourchersHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetVourchersQueryOutDto>> Handle(GetVourchersQuery request,
        CancellationToken cancellationToken)
    {
        var vouchers = (await _unit.VoucherRepository
            .GetAllAsync());
        var response = vouchers.Adapt<IEnumerable<GetVourchersQueryOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return response;
    }
}
