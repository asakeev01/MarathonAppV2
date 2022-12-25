using Domain.Common.Contracts;
using MediatR;
using System.Net;

namespace Core.UseCases.Vouchers.Commands.UpdateVoucher;


public class UpdateVoucherCommand : IRequest<HttpStatusCode>
{
    public int VoucherId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateVoucherCommandHandler : IRequestHandler<UpdateVoucherCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public UpdateVoucherCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(UpdateVoucherCommand cmd, CancellationToken cancellationToken)
    {
        var voucher = await _unit.VoucherRepository.FirstAsync(x => x.Id == cmd.VoucherId);
        voucher.Name = cmd.Name;
        voucher.isActive = cmd.IsActive;
        await _unit.VoucherRepository.Update(voucher, save: true);
        return HttpStatusCode.OK;
    }
}