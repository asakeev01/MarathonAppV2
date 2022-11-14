using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Text;

namespace Core.UseCases.Vouchers.Queries.GenerateExcelPromocodes;

public class GenerateExcelPromocodesQuery : IRequest<(byte[], string)>
{
    public int VoucherId { get; set; }
}

public class GenerateExcelPromocodesQueryHandler : IRequestHandler<GenerateExcelPromocodesQuery, (byte[], string)>
{
    private readonly IUnitOfWork _unit;

    public GenerateExcelPromocodesQueryHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<(byte[], string)> Handle(GenerateExcelPromocodesQuery request,
        CancellationToken cancellationToken)
    {

        var voucher = await _unit.VoucherRepository.FirstAsync(x => x.Id == request.VoucherId);
        var promocodes = _unit.PromocodeRepository
            .FindByCondition(predicate: x => x.VoucherId == request.VoucherId, include: source => source.Include(x => x.User));

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add(voucher.Name);

        var worksheet = excel.Workbook.Worksheets[voucher.Name];

        worksheet.Cells["A1"].Value = "Ваучер: " + voucher.Name;
        worksheet.Cells["A2"].Value = "Код";
        worksheet.Cells["B2"].Value = "Статус";
        worksheet.Cells["C2"].Value = "Пользователь";
        worksheet.Cells["D2"].Value = "Телефонный номер";
        worksheet.Cells["A1:D2"].Style.Font.Bold = true;
        worksheet.Cells["A1:D2"].Style.Font.Size = 14;
        int i = 3;
        foreach(var promocode in promocodes)
        {
            worksheet.Cells["A" + i.ToString()].Value = promocode.Code;
            if (promocode.User != null)
            {
                var user = promocode.User;
                worksheet.Cells["C" + i.ToString()].Value = $"{user.Name} {user.Surname}";
                worksheet.Cells["D" + i.ToString()].Value = user.PhoneNumber;

            }
            
            worksheet.Cells[$"A{i}:D{i}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            if (promocode.IsActivated)
            {
                worksheet.Cells["B" + i.ToString()].Value = "Использован";
                worksheet.Cells[$"A{i}:D{i}"].Style.Fill.BackgroundColor.SetColor(Color.Red);
            }
            else
            {
                worksheet.Cells["B" + i.ToString()].Value = "Не использован";
                worksheet.Cells[$"A{i}:D{i}"].Style.Fill.BackgroundColor.SetColor(Color.Green);
            }
            i += 1;
        }
        worksheet.Cells.AutoFitColumns();
        
        return (excel.GetAsByteArray(), voucher.Name);
    }
}
