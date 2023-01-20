using Domain.Common.Contracts;
using Domain.Entities.Vouchers;
using Mapster;
using MediatR;
using System.Transactions;

namespace Core.UseCases.Vouchers.Commands.CreateVoucher;


public class CreateVoucherCommand : IRequest<int>
{
    public CreateVoucherInDto VoucherDto { get; set; }
}

public class CreateVoucherCommandHandler : IRequestHandler<CreateVoucherCommand, int>
{
    private readonly IUnitOfWork _unit;

    public CreateVoucherCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<int> Handle(CreateVoucherCommand cmd, CancellationToken cancellationToken)
    {
        using var tran = await _unit.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);

        var entity = cmd.VoucherDto.Adapt<Voucher>();
        var voucher = await _unit.VoucherRepository.CreateAsync(entity, save: true);
        foreach(var slot in cmd.VoucherDto.Promocodes)
        {
            var distance = await _unit.DistanceRepository.FirstAsync(x => x.Id == slot.DistanceId);
            await _unit.PromocodeRepository.GeneratePromocodes(voucher, voucher.Marathon, distance, slot.Quantity);
            await _unit.DistanceRepository.Update(distance, save: true);
        }
        tran.Commit();
        return voucher.Id;

    }
}