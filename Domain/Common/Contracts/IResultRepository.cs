using System;
using Domain.Entities.Applications;
using Domain.Entities.Results;
using Microsoft.AspNetCore.Http;

namespace Domain.Common.Contracts;

public interface IResultRepository : IBaseRepository<Result>
{
    Task SetResultsByExcel(IQueryable<Application> applications, IFormFile excel_file, string marathonName, int marathonId);
}
