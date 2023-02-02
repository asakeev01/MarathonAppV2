using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Marathons.Exceptions;
using Domain.Entities.Vouchers.Exceptions;
using Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Transactions;

namespace Core.UseCases.Marathons.Commands.DeleteMarathon;


public class DeleteMarathonCommand : IRequest<HttpStatusCode>
{
    public int MarathonId { get; set; }
}

public class DeleteMarathonCommandHandler : IRequestHandler<DeleteMarathonCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly ISavedFileService _savedFileService;

    public DeleteMarathonCommandHandler(IUnitOfWork unit, IStringLocalizer<SharedResource> _localizer, ISavedFileService _savedFileService)
    {
        _unit = unit;
        this._localizer = _localizer;
        this._savedFileService = _savedFileService;
    }

    public async Task<HttpStatusCode> Handle(DeleteMarathonCommand cmd, CancellationToken cancellationToken)
    {
        using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var marathon = await _unit.MarathonRepository
            .FirstAsync(x => x.Id == cmd.MarathonId, include: source => source
            .Include(a => a.MarathonTranslations).ThenInclude(a => a.Logo)
            .Include(a => a.Partners).ThenInclude(a => a.PartnerCompanies).ThenInclude(a => a.Logo)
            .Include(a => a.Partners).ThenInclude(a => a.Translations)
            .Include(a => a.Distances).ThenInclude(a => a.DistancePrices)
            .Include(a => a.Distances).ThenInclude(a => a.DistanceAges)
            .Include(a => a.Documents)
            .Include(a => a.Applications)
            .Include(a => a.Vouchers)
            );

        if (marathon.Applications.Count > 0 || marathon.Vouchers.Count > 0)
        {
            throw new CantDeleteMarathonException(this._localizer);
        }
        var oldFiles = marathon.MarathonTranslations.Select(x => x.Logo).ToList();
        oldFiles.AddRange(marathon.Partners.SelectMany(x => x.PartnerCompanies.Select(y => y.Logo).ToList()));
        oldFiles.AddRange(marathon.Documents.ToList());

        foreach (var file in oldFiles)
        {
            await _savedFileService.DeleteFile(file);
        }

        foreach(var distance in marathon.Distances)
        {
            distance.DistanceAges.Clear();
            distance.DistancePrices.Clear();
        }
        marathon.Distances.Clear();
        foreach(var partner in marathon.Partners)
        {
            partner.PartnerCompanies.Clear();
            partner.Translations.Clear();
        }
        marathon.Partners.Clear();
        marathon.MarathonTranslations.Clear();
        await _unit.MarathonRepository.Delete(marathon, save: true);
        tran.Complete();
        return HttpStatusCode.OK;
    }
}