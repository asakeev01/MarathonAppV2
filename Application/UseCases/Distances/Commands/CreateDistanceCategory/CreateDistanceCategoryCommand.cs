using Core.UseCases.Marathons.Commands.CreateMarathon;
using Domain.Common.Contracts;
using Domain.Entities.Distances;
using Domain.Entities.Marathons;
using Mapster;
using MediatR;

namespace Core.UseCases.Distances.Commands.CreateDistanceCategory;

public class CreateDistanceCategoryCommand : IRequest<int>
{
    public CreateDistanceCategoryInDto distanceCategoryDto { get; set; }
}

public class CreateDistanceCategoryHandler : IRequestHandler<CreateDistanceCategoryCommand, int>
{
    private readonly IUnitOfWork _unit;

    public CreateDistanceCategoryHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<int> Handle(CreateDistanceCategoryCommand cmd, CancellationToken cancellationToken)
    {
        var entity = cmd.distanceCategoryDto.Adapt<DistanceCategory>();
        var distanceCategory = await _unit.DistanceCategoryRepository.CreateAsync(entity, save: true);
        return distanceCategory.Id;
 
    }
}