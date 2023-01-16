using Domain.Common.Contracts;
using Domain.Entities.Vouchers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Core.UseCases.Vouchers.Commands.DeleteVoucher;


public class DeleteVoucherCommand : IRequest<HttpStatusCode>
{
    public int VoucherId { get; set; }
}

public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public DeleteVoucherCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(DeleteVoucherCommand cmd, CancellationToken cancellationToken)
    {
        var voucher = await _unit.VoucherRepository.FirstAsync(x => x.Id == cmd.VoucherId, include: source => source.Include(x => x.Promocodes).ThenInclude(x => x.Distance));

        if (voucher.Promocodes.Count != 0)
        {
            throw new DeleteVoucherWithPromocodesException();
        }
        
        await _unit.VoucherRepository.Delete(voucher, save: true);

        return HttpStatusCode.OK;
    }
}