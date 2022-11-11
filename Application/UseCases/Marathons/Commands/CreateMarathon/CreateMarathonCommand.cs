using Core.UseCases.Marathons.Commands.CreateMarathon;
using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Infrastructure.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.UseCases.Marathons.Commands.CraeteMarathon;

public class CreateMarathonCommand : IRequest<int>
{
    public CreateMarathonInDto MarathonDto { get; set; }
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
        var entity = cmd.MarathonDto.Adapt<Marathon>();
        foreach(var translation in entity.MarathonTranslations)
        {
            var logo = await _savedFileService.EmptyFile();
            translation.Logo = logo;

        }
        var marathon = await _unit.MarathonRepository.CreateAsync(entity, save: true);
        return marathon.Id;
    }
}