using Core.UseCases.Marathons.Commands.CreateMarathon;
using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Mapster;
using MediatR;

namespace Core.UseCases.Marathons.Commands.CraeteMarathon;

public class CreateMarathonCommand : IRequest<int>
{
    public CreateMarathonRequestInDto marathonDto { get; set; }
}

public class CreateMarathonCommandHandler : IRequestHandler<CreateMarathonCommand, int>
{
    private readonly IUnitOfWork _unit;

    public CreateMarathonCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<int> Handle(CreateMarathonCommand cmd, CancellationToken cancellationToken)
    {
        var entity = cmd.marathonDto.Adapt<Marathon>();
        var marathon = await _unit.MarathonRepository.CreateAsync(entity, save: true);
        return marathon.Id;
 
    }
}