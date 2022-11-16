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

namespace Core.UseCases.Applications.Queries.GenerateExcelApplications;

public class GenerateExcelApplicationsQuery : IRequest<(byte[], string)>
{
    public int MarathonId { get; set; }
}

public class GenerateExcelApplicationsQueryHandler : IRequestHandler<GenerateExcelApplicationsQuery, (byte[], string)>
{
    private readonly IUnitOfWork _unit;

    public GenerateExcelApplicationsQueryHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<(byte[], string)> Handle(GenerateExcelApplicationsQuery request,
        CancellationToken cancellationToken)
    {
        var marathon = await _unit.MarathonRepository.FirstAsync(x => x.Id == request.MarathonId, include: source => source
            .Include(x => x.MarathonTranslations)
            );
        var applications = _unit.ApplicationRepository
            .FindByCondition(predicate: x => x.MarathonId == request.MarathonId, include: source => source
            .Include(x => x.User).ThenInclude(x => x.Status)
            .Include(x => x.Promocode).ThenInclude(x => x.Voucher)
            .Include(x => x.DistanceForPWD)
            .Include(x => x.Distance)
            );

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        ExcelPackage excel = new ExcelPackage();
        var marathonName = marathon.MarathonTranslations.Where(x => x.LanguageId == 2).First().Name;
        excel.Workbook.Worksheets.Add(marathonName);

        var worksheet = excel.Workbook.Worksheets[marathonName];

        worksheet.Cells["A1"].Value = "Id";
        worksheet.Cells["B1"].Value = "Магнит";
        worksheet.Cells["C1"].Value = "Номер";
        worksheet.Cells["D1"].Value = "Имя";
        worksheet.Cells["E1"].Value = "Фамилия";
        worksheet.Cells["F1"].Value = "Почта";
        worksheet.Cells["G1"].Value = "Пол";
        worksheet.Cells["H1"].Value = "Дата рождения";
        worksheet.Cells["I1"].Value = "Страна";
        worksheet.Cells["J1"].Value = "Телефон";
        worksheet.Cells["K1"].Value = "Дистанция";
        worksheet.Cells["L1"].Value = "Способ оплаты";
        worksheet.Cells["M1"].Value = "ЛОВЗ";
        worksheet.Cells["A1:M1"].Style.Font.Bold = true;
        worksheet.Cells["A1:M1"].Style.Font.Size = 14;
        int i = 2;
        foreach (var application in applications)
        {
            worksheet.Cells[$"A{i}"].Value = application.Id;
            worksheet.Cells[$"B{i}"].Value = "";
            worksheet.Cells[$"C{i}"].Value = application.Number;
            worksheet.Cells[$"D{i}"].Value = application.User.Name;
            worksheet.Cells[$"E{i}"].Value = application.User.Surname;
            worksheet.Cells[$"F{i}"].Value = application.User.Email;
            worksheet.Cells[$"G{i}"].Value = application.User.Gender;
            worksheet.Cells[$"H{i}"].Value = application.User.DateOfBirth.Value.ToString("dd/MM/yyyy");
            worksheet.Cells[$"I{i}"].Value = application.User.Country.Value;
            worksheet.Cells[$"J{i}"].Value = application.User.PhoneNumber;

            if (application.User.Gender == true) worksheet.Cells[$"G{i}"].Value = "Муж";
            else worksheet.Cells[$"G{i}"].Value = "Жен";

            if (application.Payment == Domain.Entities.Applications.ApplicationEnums.PaymentMethodEnum.PWD)
            {
                worksheet.Cells[$"L{i}"].Value = "ЛОВЗ";
                worksheet.Cells[$"M{i}"].Value = "Да";
                worksheet.Cells[$"K{i}"].Value = application.DistanceForPWD.Name;
            }
            else if (application.Payment == Domain.Entities.Applications.ApplicationEnums.PaymentMethodEnum.Voucher)
            {
                worksheet.Cells[$"L{i}"].Value = $"Ваучер - {application.Promocode.Voucher.Name}" ;
                worksheet.Cells[$"M{i}"].Value = "Нет";
                worksheet.Cells[$"K{i}"].Value = application.Distance.Name;
            }
            else if (application.Payment == Domain.Entities.Applications.ApplicationEnums.PaymentMethodEnum.Money)
            {
                worksheet.Cells[$"L{i}"].Value = "Деньги";
                worksheet.Cells[$"M{i}"].Value = "Нет";
                worksheet.Cells[$"K{i}"].Value = application.Distance.Name;
            };

            i += 1;
        }
        worksheet.Cells.AutoFitColumns();
        worksheet.Protection.IsProtected = true;
        worksheet.Cells[$"B2:B{i}"].Style.Locked = false;

        return (excel.GetAsByteArray(), marathonName);
    }
}
