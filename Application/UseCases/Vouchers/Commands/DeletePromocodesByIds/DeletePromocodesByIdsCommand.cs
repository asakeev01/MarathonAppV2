using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Vouchers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Net;


namespace Core.UseCases.Vouchers.Commands.DeletePromocodesByIds;


public class DeletePromocodesByIdsCommand : IRequest<HttpStatusCode>
{
    public int VoucherId { get; set; }
    public ICollection<int> PromocodesIds { get; set; }
}

public class DeletePromocodesByIdsCommandHandler : IRequestHandler<DeletePromocodesByIdsCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private IStringLocalizer<SharedResource> _localizer;
    public DeletePromocodesByIdsCommandHandler(IUnitOfWork unit, IStringLocalizer<SharedResource> localizer)
    {
        _unit = unit;
        _localizer = localizer;
    }

    public async Task<HttpStatusCode> Handle(DeletePromocodesByIdsCommand cmd, CancellationToken cancellationToken)
    {
        var voucher = await _unit.VoucherRepository.FirstAsync(x => x.Id == cmd.VoucherId, include: source => source.Include(x => x.Promocodes).ThenInclude(x => x.Distance));

        var promocodes = voucher.Promocodes.Where(x => cmd.PromocodesIds.Contains(x.Id));
        if (promocodes.Count(x => x.IsActivated == true) >= 1)
        {
            throw new DeleteActivatedPromocodeException(_localizer);
        }

        foreach (var promocode in promocodes)
        {
            promocode.Distance.ReservedPlaces -= 1;
            await _unit.PromocodeRepository.Delete(promocode, save: true);
        }

        return HttpStatusCode.OK;
    }
}