using Domain.Common.Contracts;
using Domain.Entities.Vouchers;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Transactions;

namespace Core.UseCases.Vouchers.Commands.AddPromocodesToVoucher;


public class AddPromocodesToVoucherCommand : IRequest<HttpStatusCode>
{
    public int VoucherId { get; set; }
    public AddPromocodesToVoucherCommandInDto PromocodesDto { get; set; }
}

public class AddPromocodesToVoucherCommandHandler : IRequestHandler<AddPromocodesToVoucherCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public AddPromocodesToVoucherCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(AddPromocodesToVoucherCommand cmd, CancellationToken cancellationToken)
    {
        using var tran = await _unit.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);

        var voucher = await _unit.VoucherRepository.FirstAsync(x => x.Id == cmd.VoucherId, include: source => source.Include(x => x.Promocodes));
        foreach (var slot in cmd.PromocodesDto.Promocodes)
        {
            var distance = await _unit.DistanceRepository.FirstAsync(x => x.Id == slot.DistanceId);
            await _unit.PromocodeRepository.GeneratePromocodes(voucher, voucher.Marathon, distance, slot.Quantity);
            await _unit.DistanceRepository.Update(distance, save: true);
        }
        tran.Commit();
        return HttpStatusCode.OK;
    }
}