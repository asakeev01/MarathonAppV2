using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public class GetPromocodesByVaucherIdQuery : IRequest<GetPromocodesByVaucherIdQueryOutDto>
{
    public int VoucherId { get; set; }
    public GridifyQuery Query { get; set; }
}

public class GetVourcherHandler : IRequestHandler<GetPromocodesByVaucherIdQuery, GetPromocodesByVaucherIdQueryOutDto>
{
    private readonly IUnitOfWork _unit;

    public GetVourcherHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<GetPromocodesByVaucherIdQueryOutDto> Handle(GetPromocodesByVaucherIdQuery request,
        CancellationToken cancellationToken)
    {

        var voucher = await _unit.VoucherRepository.FirstAsync(x => x.Id == request.VoucherId);

        var promocodes = _unit.PromocodeRepository
            .FindByCondition(predicate:x => x.VoucherId == request.VoucherId, include: source => source.Include(x => x.Distance).Include(x => x.User));
        
        var promocodesDto = promocodes.Adapt<IEnumerable<GetPromocodesByVaucherIdQueryOutDto.PromocodeDto>>().AsQueryable().GridifyQueryable(request.Query);

        var result = new GetPromocodesByVaucherIdQueryOutDto();
        result.Promocodes = promocodesDto;
        result.VoucherName = voucher.Name;
        result.Id = voucher.Id;
        return result;
    }
}
