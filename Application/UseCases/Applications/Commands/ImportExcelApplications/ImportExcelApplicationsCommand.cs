using Core.UseCases.Marathons.Commands.CreateMarathon;
using Domain.Common.Contracts;
using Domain.Entities.Applications.Exceptions;
using Domain.Entities.Marathons;
using Domain.Entities.Users;
using Domain.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Net;

namespace Core.UseCases.Applications.Commands.ImportExcelApplications;

public class ImportExcelApplicationsCommand : IRequest<HttpStatusCode>
{
    public int MarathonId { get; set; }
    public IFormFile ExcelFile { get; set; }
}

public class ImportExcelApplicationsCommandHandler : IRequestHandler<ImportExcelApplicationsCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public ImportExcelApplicationsCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(ImportExcelApplicationsCommand cmd, CancellationToken cancellationToken)
    {
        var marathon = await _unit.MarathonRepository.FirstAsync(x => x.Id == cmd.MarathonId, include: source => source
            .Include(x => x.MarathonTranslations));

        var marathonName = marathon.MarathonTranslations.Where(x => x.LanguageId == 2).First().Name;

        var applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.MarathonId == cmd.MarathonId);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var stream = new MemoryStream())
        {
            await cmd.ExcelFile.CopyToAsync(stream, cancellationToken);
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[marathonName];
                if (worksheet == null) throw new InvalidSheetNameException();
                if (worksheet.Cells["A1"].Value.ToString() != "Id") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["B1"].Value.ToString() != "Магнит") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["C1"].Value.ToString() != "Номер") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["D1"].Value.ToString() != "Имя") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["E1"].Value.ToString() != "Фамилия") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["F1"].Value.ToString() != "Почта") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["G1"].Value.ToString() != "Пол") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["H1"].Value.ToString() != "Дата рождения") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["I1"].Value.ToString() != "Страна") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["J1"].Value.ToString() != "Телефон") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["K1"].Value.ToString() != "Дистанция") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["L1"].Value.ToString() != "Способ оплаты") throw new InvalidHeadersInExcelException();
                if (worksheet.Cells["M1"].Value.ToString() != "ЛОВЗ") throw new InvalidHeadersInExcelException();

                int i = 2;
                while (worksheet.Cells[$"A{i}"].Value != null)
                {
                    if (worksheet.Cells[$"B{i}"].Value == null)
                    {
                        applications.Where(x => x.Id == Convert.ToUInt32(worksheet.Cells[$"A{i}"].Value.ToString())).First().Magnet = null;
                    }
                    else
                    {
                        applications.Where(x => x.Id == Convert.ToUInt32(worksheet.Cells[$"A{i}"].Value.ToString())).First().Magnet = worksheet.Cells[$"B{i}"].Value.ToString();
                    }
                    i++;
                }

            }
            await _unit.ApplicationRepository.SaveAsync();
        }

        return HttpStatusCode.OK;
    }
}