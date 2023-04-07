using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Applications;
using Domain.Entities.Applications.Exceptions;
using Domain.Entities.Distances;
using Domain.Entities.Marathons;
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

    public async Task SetResultsByExcel(IQueryable<Application> applications, IFormFile excel_file, Marathon marathon)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var stream = new MemoryStream())
        {
            await excel_file.CopyToAsync(stream);
            var marathonName = marathon.MarathonTranslations.Where(x => x.LanguageId == 1).First().Name;
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
                var results = FindByCondition(x => x.Application.MarathonId == marathon.Id, include: source => source.Include(x => x.Application));

                Dictionary<int, int> distanceCount = new Dictionary<int, int>();
                Dictionary<(int, int?, bool?), int> categoryCount = new Dictionary<(int, int?, bool?), int>();

                foreach (var distance in marathon.Distances)
                {
                    distanceCount.Add(distance.Id, applications.Where(x => x.IsPWD != true && x.DistanceId == distance.Id).Count());
                    distanceCount.Add(-distance.Id, applications.Where(x => x.IsPWD == true && x.DistanceId == distance.Id).Count());

                    foreach(var distanceAge in distance.DistanceAges)
                    {
                        categoryCount.Add((distance.Id, distanceAge.Id, distanceAge.Gender),applications.Where(x => x.IsPWD != true && x.DistanceAgeId == distanceAge.Id && x.User.Gender == distanceAge.Gender).Count());
                    }
                    categoryCount.Add((distance.Id, -1, true), applications.Where(x => x.IsPWD == true && x.User.Gender == true).Count());
                    categoryCount.Add((distance.Id, -1, false), applications.Where(x => x.IsPWD == true && x.User.Gender == false).Count());
                }

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
                            GeneralCount = application.DistanceAgeId == null ? distanceCount[-application.Distance.Id] : distanceCount[application.Distance.Id],
                            CategoryCount = application.DistanceAgeId == null ? categoryCount[(application.Distance.Id, -1, application.User.Gender)] : categoryCount[(application.Distance.Id, application.DistanceAgeId, application.User.Gender)],

                        };
                        await CreateAsync(result);
                    }
                    else
                    {
                        result.CategoryPlace = worksheet.Cells[$"B{i}"].Value.ToString();
                        result.GeneralPlace = worksheet.Cells[$"C{i}"].Value.ToString();
                        result.GunTime = worksheet.Cells[$"D{i}"].Value.ToString();
                        result.ChipTime = worksheet.Cells[$"E{i}"].Value.ToString();
                        result.GeneralCount = application.DistanceAgeId == null ? distanceCount[-application.Distance.Id] : distanceCount[application.Distance.Id];
                        result.CategoryCount = application.DistanceAgeId == null ? categoryCount[(application.Distance.Id, -1, application.User.Gender)] : categoryCount[(application.Distance.Id, application.DistanceAgeId, application.User.Gender)];

                        await Update(result);
                    }
                    i++;
                }
            }
            await SaveAsync();
        }
    }
}
