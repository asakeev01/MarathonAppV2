using Core.Common.Helpers;
using Core.UseCases.Marathons.Queries.GetMarathons;
using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Marathons.Queries.GetMarathon;

public class GetMarathonsQuery : IRequest<QueryablePaging<GetMarathonsOutDto>>
{
    public GridifyQuery Query { get; set; }

    public string LanguageCode { get; set; }
}

public class GetMarathonsHandler : IRequestHandler<GetMarathonsQuery, QueryablePaging<GetMarathonsOutDto>>
{
    private readonly IMarathonRepository _marathonRepository;

    public GetMarathonsHandler(IMarathonRepository marathonRepository)
    {
        _marathonRepository = marathonRepository;
    }

    public async Task<QueryablePaging<GetMarathonsOutDto>> Handle(GetMarathonsQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);
        var marathons = (await _marathonRepository
            .GetAllAsync(include: source => source.Include(a => a.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode))));
        var response = marathons.Adapt<IEnumerable<GetMarathonsOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return response;
    }
}