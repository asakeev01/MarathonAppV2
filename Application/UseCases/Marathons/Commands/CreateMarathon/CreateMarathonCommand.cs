using System.Net;
using Core.UseCases.Marathons.Commands.CreateMarathon;
using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Domain.Services.Interfaces;
using Mapster;
using MediatR;

namespace Core.UseCases.Marathons.Commands.CraeteMarathon;

public class CreateMarathonCommand : IRequest<int>
{
    public CreateMarathonRequestInDto marathonDto { get; set; }
}

public class CreateMarathonCommandHandler : IRequestHandler<CreateMarathonCommand, int>
{
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unit;

    public CreateMarathonCommandHandler(IAccountService accountService, IUnitOfWork unit)
    {
        _accountService = accountService;
        _unit = unit;
    }

    public async Task<int> Handle(CreateMarathonCommand cmd, CancellationToken cancellationToken)
    {
        var entity = cmd.marathonDto.Adapt<Marathon>();
        var marathon = await _unit.MarathonRepository.CreateAsync(entity, save: true);

        var entranslation = new MarathonTranslation
        {
            Name = cmd.marathonDto.NameEn,
            Text = cmd.marathonDto.TextEn,
            Place = cmd.marathonDto.PlaceEn,
            Marathon = marathon,
            LanguageId = 1,
        };
        var rutranslation = new MarathonTranslation
        {
            Name = cmd.marathonDto.NameRu,
            Text = cmd.marathonDto.TextRu,
            Place = cmd.marathonDto.PlaceRu,
            Marathon = marathon,
            LanguageId = 2,
        };
        var kgtranslation = new MarathonTranslation
        {
            Name = cmd.marathonDto.NameKg,
            Text = cmd.marathonDto.TextKg,
            Place = cmd.marathonDto.PlaceKg,
            Marathon = marathon,
            LanguageId = 3,
        };
        await _unit.MarathonTranslationRepository.CreateAsync(entranslation, save: true);
        await _unit.MarathonTranslationRepository.CreateAsync(rutranslation, save: true);
        await _unit.MarathonTranslationRepository.CreateAsync(kgtranslation, save: true);

        return marathon.Id;
    }
}