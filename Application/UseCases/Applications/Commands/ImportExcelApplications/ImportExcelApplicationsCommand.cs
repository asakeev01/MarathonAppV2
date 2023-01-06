using Domain.Common.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        var marathonName = marathon.MarathonTranslations.Where(x => x.LanguageId == 1).First().Name;

        var applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.MarathonId == cmd.MarathonId);

        await _unit.ApplicationRepository.ImportExcel(applications, cmd.ExcelFile, marathonName);

        return HttpStatusCode.OK;
    }
}