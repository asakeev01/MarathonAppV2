using Domain.Common.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Transactions;

namespace Core.UseCases.Results.Commands.SetResultsByExcel;

public class SetResultByExcelCommand : IRequest<HttpStatusCode>
{
    public int MarathonId { get; set; }
    public IFormFile ExcelFile { get; set; }
}

public class SetResultByExcelHandler : IRequestHandler<SetResultByExcelCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public SetResultByExcelHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(SetResultByExcelCommand cmd, CancellationToken cancellationToken)
    {
        var options = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromMinutes(2)
        };
        using var tran = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
            var marathon = await _unit.MarathonRepository.FirstAsync(x => x.Id == cmd.MarathonId, include: source => source
            .Include(x => x.MarathonTranslations));

        var marathonName = marathon.MarathonTranslations.Where(x => x.LanguageId == 1).First().Name;

        var applications = _unit.ApplicationRepository.FindByCondition(predicate: x => x.MarathonId == cmd.MarathonId);

        await _unit.ResultRepository.SetResultsByExcel(applications, cmd.ExcelFile, marathonName, marathon.Id);

        tran.Complete();

        return HttpStatusCode.OK;
    }
}