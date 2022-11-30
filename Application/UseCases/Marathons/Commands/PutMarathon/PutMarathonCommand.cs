using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Domain.Entities.SavedFiles;
using Infrastructure.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Transactions;

namespace Core.UseCases.Marathons.Commands.PutMarathon;

public class PutMarathonCommand : IRequest<HttpStatusCode>
{
    public PutMarathonInDto MarathonDto { get; set; }
    public ICollection<IFormFile> Documents { get; set; }
    public List<UpdatePartnersLogos> PartnersLogo { get; set; }
    public List<UpdateMarathonLogos> MarathonLogo { get; set; }
}

public class PutMarathonCommandHandler : IRequestHandler<PutMarathonCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedFileService _savedFileService;

    public PutMarathonCommandHandler(IUnitOfWork unit, ISavedFileService savedFileService)
    {
        _unit = unit;
        _savedFileService = savedFileService;
    }

    public async Task<HttpStatusCode> Handle(PutMarathonCommand cmd, CancellationToken cancellationToken)
    {
        using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var marathon = await _unit.MarathonRepository
            .FirstAsync(x => x.Id == cmd.MarathonDto.Id, include: source => source
            .Include(a => a.MarathonTranslations).ThenInclude(a => a.Logo)
            .Include(a => a.DistancesForPWD)
            .Include(a => a.Partners).ThenInclude(a => a.Logos)
            .Include(a => a.Partners).ThenInclude(a => a.Translations)
            .Include(a => a.Distances).ThenInclude(a => a.DistancePrices)
            .Include(a => a.Distances).ThenInclude(a => a.DistanceAges)
            .Include(a => a.Documents)
            );

        var oldMarathonLogos = marathon.MarathonTranslations.Select(x => x.Logo).ToList();

        foreach (var partner in marathon.Partners)
        {
            foreach (var logo in partner.Logos)
            {
                await _savedFileService.DeleteFile(logo);
            }
        }

        cmd.MarathonDto.Adapt(marathon);


        foreach (var translation in marathon.MarathonTranslations)
        {
            var logo = translation.Logo;
            var file = cmd.MarathonLogo.Where(x => x.LanguageId == translation.LanguageId).First().Logo;
            var newLogo = await _savedFileService.UploadFile(file, Domain.Common.Constants.FileTypeEnum.Marathons);
            translation.Logo = newLogo;
        }

        foreach(var oldLogo in oldMarathonLogos)
        {
            await _savedFileService.DeleteFile(oldLogo);
        }

        foreach (var document in marathon.Documents)
        {
            await _savedFileService.DeleteFile(document);
        }


        foreach (var document in cmd.Documents)
        {
            var fileDocument = await _savedFileService.UploadFile(document, Domain.Common.Constants.FileTypeEnum.Documents);
            fileDocument.Marathon = marathon;
        }


        foreach (var partner in cmd.PartnersLogo)
        {
            var entityParner = marathon.Partners.Where(x => x.SerialNumber == partner.SerialNumber).First();

            foreach (var logo in partner.Logos)
            {
                var fileLogo = await _savedFileService.UploadFile(logo, Domain.Common.Constants.FileTypeEnum.Partners);
                fileLogo.Partner = entityParner;
            }
        }

        await _unit.SavedFileRepository.SaveAsync();
        await _unit.MarathonRepository.Update(marathon, save: true);
        tran.Complete();
        return HttpStatusCode.OK;
    }
}
