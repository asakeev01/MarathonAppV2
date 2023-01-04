using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Applications;
using Domain.Entities.Applications.Exceptions;
using Domain.Entities.Marathons;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;

namespace Infrastructure.Persistence.Repositories;

public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
{
    public ApplicationRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }

    public async Task<byte[]> GenerateExcel(IQueryable<Application> applications, string marathonName)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        ExcelPackage excel = new ExcelPackage();
        excel.Workbook.Worksheets.Add(marathonName);

        var worksheet = excel.Workbook.Worksheets[marathonName];

        for (int j = 0; j < AppConstants.ApplicationExcelColumns.Count; j++)
        {
            worksheet.Cells[AppConstants.ApplicationExcelColumns[j].Item1].Value = AppConstants.ApplicationExcelColumns[j].Item2;
        }

        worksheet.Cells["A1:M1"].Style.Font.Bold = true;
        worksheet.Cells["A1:M1"].Style.Font.Size = 14;
        int i = 2;
        foreach (var application in applications)
        {
            worksheet.Cells[$"A{i}"].Value = application.Id;
            worksheet.Cells[$"B{i}"].Value = application.Magnet;
            worksheet.Cells[$"C{i}"].Value = application.Number;
            worksheet.Cells[$"D{i}"].Value = application.User.Name;
            worksheet.Cells[$"E{i}"].Value = application.User.Surname;
            worksheet.Cells[$"F{i}"].Value = application.User.Email;
            worksheet.Cells[$"G{i}"].Value = application.User.DateOfBirth.Value.ToString("dd/MM/yyyy");
            worksheet.Cells[$"H{i}"].Value = application.User.Country.Value;
            worksheet.Cells[$"I{i}"].Value = application.User.PhoneNumber;


            if (application.User.Gender == true) worksheet.Cells[$"J{i}"].Value = "Муж";
            else worksheet.Cells[$"J{i}"].Value = "Жен";

            if (application.Payment == Domain.Entities.Applications.ApplicationEnums.PaymentMethodEnum.PWD)
            {
                worksheet.Cells[$"L{i}"].Value = "ЛОВЗ";
                worksheet.Cells[$"M{i}"].Value = "Да";
                worksheet.Cells[$"K{i}"].Value = application.DistanceForPWD.Name;
            }
            else if (application.Payment == Domain.Entities.Applications.ApplicationEnums.PaymentMethodEnum.Voucher)
            {
                worksheet.Cells[$"L{i}"].Value = $"Ваучер - {application.Promocode.Voucher.Name}";
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

        return excel.GetAsByteArray();
    }

    public async Task ImportExcel(IQueryable<Application> applications, IFormFile excel_file, string marathonName)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var stream = new MemoryStream())
        {
            await excel_file.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[marathonName];
                if (worksheet == null) throw new InvalidSheetNameException();

                for (int j = 0; j < AppConstants.ApplicationExcelColumns.Count; j++)
                {
                    if (worksheet.Cells[AppConstants.ApplicationExcelColumns[j].Item1].Value.ToString() != AppConstants.ApplicationExcelColumns[j].Item2)
                        throw new InvalidHeadersInExcelException();
                }

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
            await SaveAsync();
        }
    }

}