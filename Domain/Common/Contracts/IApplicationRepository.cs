using Domain.Entities.Applications;
using Domain.Entities.Marathons;
using Microsoft.AspNetCore.Http;

namespace Domain.Common.Contracts;
public interface IApplicationRepository : IBaseRepository<Application>
{
    Task<byte[]> GenerateExcel(IQueryable<Application> applications, string marathonName);
    Task ImportExcel(IQueryable<Application> applications, IFormFile excel_file,  string marathonName);
}
