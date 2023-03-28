using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Applications;
using Domain.Entities.Applications.Exceptions;
using Domain.Entities.Results;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;

namespace Infrastructure.Persistence.Repositories;

public class ResultRepository : BaseRepository<Result>, IResultRepository
{
    private IStringLocalizer<SharedResource> _localizer;
    public ResultRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
        this._localizer = localizer;
    }

    public async Task SetResultsByExcel(IQueryable<Application> applications, IFormFile excel_file, string marathonName, int marathonId)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var stream = new MemoryStream())
        {
            await excel_file.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[marathonName];
                if (worksheet == null) throw new InvalidSheetNameException(_localizer);

                for (int j = 0; j < AppConstants.ResultsExcelColumns.Count; j++)
                {
                    if (worksheet.Cells[AppConstants.ApplicationExcelColumns[j].Item1].Value.ToString() != AppConstants.ResultsExcelColumns[j].Item2)
                        throw new InvalidHeadersInExcelException(_localizer);
                }

                int i = 2;
                var results = FindByCondition(x => x.Application.MarathonId == marathonId);

                while (worksheet.Cells[$"A{i}"].Value != null)
                {
                    var application = await applications.FirstOrDefaultAsync(x => x.Number.ToString() == worksheet.Cells[$"A{i}"].Value.ToString());
                    if (application == null)
                        throw new NotFoundApplicationException(_localizer, worksheet.Cells[$"A{i}"].Value.ToString());
                   
                    var result = results.Where(x => x.ApplicationId == application.Id).FirstOrDefault();
                    if (result == null)
                    {
                        result = new Result()
                        {
                            ApplicationId = application.Id,
                            CategoryPlace = worksheet.Cells[$"B{i}"].Value.ToString(),
                            GeneralPlace = worksheet.Cells[$"C{i}"].Value.ToString(),
                            GunTime = worksheet.Cells[$"D{i}"].Value.ToString(),
                            ChipTime = worksheet.Cells[$"E{i}"].Value.ToString(),

                        };
                        await CreateAsync(result);
                    }
                    else
                    {
                        result.CategoryPlace = worksheet.Cells[$"B{i}"].Value.ToString();
                        result.GeneralPlace = worksheet.Cells[$"C{i}"].Value.ToString();
                        result.GunTime = worksheet.Cells[$"D{i}"].Value.ToString();
                        result.ChipTime = worksheet.Cells[$"E{i}"].Value.ToString();
                        await Update(result);
                    }
                    i++;
                }
            }
            await SaveAsync();
        }
    }
}
