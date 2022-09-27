using Core.Common.Helpers;
using Core.UseCases.Marathons.Queries.GetMarathons;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Accounts.Queries.GetUserAccount;

public class GetMarathonsQuery : IRequest<List<GetMarathonOUTDTO>>
{
    public GridifyQuery Query { get; set; }

    public string LanguageCode { get; set; }
}

public class GetMarathonsHandler : IRequestHandler<GetMarathonsQuery, List<GetMarathonOUTDTO>>
{
    private readonly IMarathonRepository _marathonRepository;

    public GetMarathonsHandler(IMarathonRepository marathonRepository)
    {
        _marathonRepository = marathonRepository;
    }

    public async Task<List<GetMarathonOUTDTO>> Handle(GetMarathonsQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);
        var marathons = (await _marathonRepository
            .GetAllAsync(include: source => source.Include(a=> a.MarathonTranslations.Where(t=> t.Language.Code == request.LanguageCode)))).ToList();

        var res1 = marathons.Adapt<List<GetMarathonOUTDTO>>();


        return res1;
    }
}