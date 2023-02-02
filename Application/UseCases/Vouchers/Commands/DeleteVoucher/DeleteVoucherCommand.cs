using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Vouchers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Net;


namespace Core.UseCases.Vouchers.Commands.DeleteVoucher;


public class DeleteVoucherCommand : IRequest<HttpStatusCode>
{
    public int VoucherId { get; set; }
}

public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public DeleteVoucherCommandHandler(IUnitOfWork unit, IStringLocalizer<SharedResource> _localizer)
    {
        _unit = unit;
        this._localizer = _localizer;
    }

    public async Task<HttpStatusCode> Handle(DeleteVoucherCommand cmd, CancellationToken cancellationToken)
    {
        var voucher = await _unit.VoucherRepository.FirstAsync(x => x.Id == cmd.VoucherId, include: source => source.Include(x => x.Promocodes).ThenInclude(x => x.Distance));

        if (voucher.Promocodes.Count != 0)
        {
            throw new DeleteVoucherWithPromocodesException(_localizer);
        }
        
        await _unit.VoucherRepository.Delete(voucher, save: true);

        return HttpStatusCode.OK;
    }
}