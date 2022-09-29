using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Distances.Queries.GetDistanceCategories;

public class GetDistanceCategoriesAdminQuery : IRequest<GetDistanceCategoriesAdminOutDto>
{
    public GridifyQuery Query { get; set; }
    public int DistanceCategoryId { get; set; }
}

public class GetDistanceCategoriesAdminHandler : IRequestHandler<GetDistanceCategoriesAdminQuery, GetDistanceCategoriesAdminOutDto>
{
    private readonly IUnitOfWork _unit;

    public GetDistanceCategoriesAdminHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<GetDistanceCategoriesAdminOutDto> Handle(GetDistanceCategoriesAdminQuery request,
        CancellationToken cancellationToken)
    {
        var distanceCategory = (await _unit.DistanceCategoryRepository
            .FirstToAsync<GetDistanceCategoriesAdminOutDto>(x => x.Id == request.DistanceCategoryId, include: source => source.Include(a => a.DistanceCategoryTranslations)));
        return distanceCategory;
    }
}