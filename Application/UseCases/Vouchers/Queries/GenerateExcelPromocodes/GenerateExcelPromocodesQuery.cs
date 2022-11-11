using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Core.UseCases.Vouchers.Queries.GenerateExcelPromocodes;

public class GenerateExcelPromocodesQuery : IRequest<byte[]>
{
    public int VoucherId { get; set; }
}

public class GenerateExcelPromocodesQueryHandler : IRequestHandler<GenerateExcelPromocodesQuery, byte[]>
{
    private readonly IUnitOfWork _unit;

    public GenerateExcelPromocodesQueryHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<byte[]> Handle(GenerateExcelPromocodesQuery request,
        CancellationToken cancellationToken)
    {

        var voucher = await _unit.VoucherRepository.FirstAsync(x => x.Id == request.VoucherId);
        var promocodes = _unit.PromocodeRepository
            .FindByCondition(predicate: x => x.VoucherId == request.VoucherId);

        StringBuilder str = new StringBuilder();
        str.Append("<b><font face=Arial Narrow size=3>" + "Voucher = " +  voucher.Name + "</font></b>");
        str.Append("<table border=`" + "1px" + "`b>");
        str.Append("<tr>");
        str.Append("<td><b><font face=Arial Narrow size=3>Promocode</font></b></td>");
        str.Append("<td><b><font face=Arial Narrow size=3>IsActivated</font></b></td>");
        str.Append("</tr>");

        foreach(var promocode in promocodes)
        {
            str.Append("<tr>");
            str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + promocode.Code + "</font></td>");
            str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + promocode.IsActivated.ToString() + "</font></td>");
            str.Append("</tr>");
        }
        str.Append("</table>");
        byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
        return temp;
    }
}
