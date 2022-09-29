using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Distances.Queries.GetDistanceCategories;

public class GetDistanceCategoriesQuery : IRequest<QueryablePaging<GetDistanceCategoriesOutDto>>
{
    public GridifyQuery Query { get; set; }

    public string LanguageCode { get; set; }
}

public class GetDistanceCategoriesHandler : IRequestHandler<GetDistanceCategoriesQuery, QueryablePaging<GetDistanceCategoriesOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetDistanceCategoriesHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetDistanceCategoriesOutDto>> Handle(GetDistanceCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);
        var distanceCategories = (await _unit.DistanceCategoryRepository
            .GetAllAsync(include: source => source.Include(a => a.DistanceCategoryTranslations.Where(t => t.Language.Code == request.LanguageCode))));
        var response = distanceCategories.Adapt<IEnumerable<GetDistanceCategoriesOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return response;
    }
}