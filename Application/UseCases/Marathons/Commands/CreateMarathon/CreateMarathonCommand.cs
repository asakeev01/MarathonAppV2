﻿using Core.UseCases.Marathons.Commands.CreateMarathon;
using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Infrastructure.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Transactions;

namespace Core.UseCases.Marathons.Commands.CraeteMarathon;

public class CreateMarathonCommand : IRequest<int>
{
    public CreateMarathonInDto MarathonDto { get; set; }
    public ICollection<IFormFile> Documents { get; set; }
    public List<PartnerCompanyLogo> PartnerCompanyLogos { get; set; }
    public List<MarathonLogos> MarathonLogo { get; set; }
}

public class CreateMarathonCommandHandler : IRequestHandler<CreateMarathonCommand, int>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedFileService _savedFileService;

    public CreateMarathonCommandHandler(IUnitOfWork unit, ISavedFileService savedFileService)
    {
        _unit = unit;
        _savedFileService = savedFileService;
    }

    public async Task<int> Handle(CreateMarathonCommand cmd, CancellationToken cancellationToken)
    {
        using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var entity = cmd.MarathonDto.Adapt<Marathon>();
        var marathon = await _unit.MarathonRepository.CreateAsync(entity, save: true);

        foreach (var document in cmd.Documents)
        {
            var fileDocument = await _savedFileService.UploadFile(document, Domain.Common.Constants.FileTypeEnum.Documents);
            fileDocument.Marathon = marathon;
        }

        foreach (var company in cmd.PartnerCompanyLogos)
        {
            var entityCompany = marathon.Partners.Where(x => x.SerialNumber == company.SerialNumber).First().PartnerCompanies.Where(x => x.Name == company.Name).First();
            var fileLogo = await _savedFileService.UploadFile(company.Logo, Domain.Common.Constants.FileTypeEnum.Partners);
            entityCompany.Logo = fileLogo;
           
        }

        foreach (var translation in cmd.MarathonLogo)
        {
            var entityTranslation = marathon.MarathonTranslations.Where(x => x.LanguageId == translation.LanguageId).First();
            var fileLogo = await _savedFileService.UploadFile(translation.Logo, Domain.Common.Constants.FileTypeEnum.Marathons);
            entityTranslation.Logo = fileLogo;
        }

        await _unit.SavedFileRepository.SaveAsync();
        await _unit.MarathonRepository.Update(marathon, save: true);

        tran.Complete();

        return marathon.Id;
    }
}