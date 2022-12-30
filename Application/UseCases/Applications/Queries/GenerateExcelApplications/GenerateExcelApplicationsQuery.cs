using Core.Common.Helpers;
using Domain.Common.Constants;
using Domain.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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

        var marathonName = marathon.MarathonTranslations.Where(x => x.LanguageId == 1).First().Name;
        var byte_excel = await _unit.ApplicationRepository.GenerateExcel(applications, marathonName);

        return (byte_excel, marathonName);
    }
}
