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
    public CreateMarathonRequestInDto marathonDto { get; set; }
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
        var entity = cmd.marathonDto.Adapt<Marathon>();
        var marathon = await _unit.MarathonRepository.CreateAsync(entity, save: true);
        //var logo = await _savedFileService.UploadFile(cmd.logo);
        //marathon.Logo = logo;
        return marathon.Id;
 
    }
}