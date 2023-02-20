using Domain.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Core.UseCases.Vouchers.Commands.DeleteNonActivatedPromocodes;


public class DeleteNonActivatedPromocodesCommand : IRequest<HttpStatusCode>
{
    public int VoucherId { get; set; }
}

public class DeleteNonActivatedPromocodesCommandHandler : IRequestHandler<DeleteNonActivatedPromocodesCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public DeleteNonActivatedPromocodesCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(DeleteNonActivatedPromocodesCommand cmd, CancellationToken cancellationToken)
    {
        var voucher = await _unit.VoucherRepository.FirstAsync(x => x.Id == cmd.VoucherId, include: source => source.Include(x => x.Promocodes).ThenInclude(x => x.Distance));

        var nonActivatedPromocodes = voucher.Promocodes.Where(x => x.IsActivated == false).GroupBy(x => x.Distance);

        foreach(var group in nonActivatedPromocodes)
        {
            var distance = group.FirstOrDefault().Distance;

            distance.ReservedPlaces -= group.Count();
            var t = group.ToList();
            await _unit.PromocodeRepository.BulkDeleteAsync(t);
        }

        await _unit.SaveAsync();
        
        return HttpStatusCode.OK;
    }
}