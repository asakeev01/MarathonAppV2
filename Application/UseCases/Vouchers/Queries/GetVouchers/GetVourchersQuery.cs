using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Vouchers.Queries.GetVouchers;

public class GetVourchersQuery : IRequest<QueryablePaging<GetVourchersQueryOutDto>>
{
    public string LanguageCode { get; set; }
    public GridifyQuery Query { get; set; }
}

public class GetVourchersHandler : IRequestHandler<GetVourchersQuery, QueryablePaging<GetVourchersQueryOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetVourchersHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetVourchersQueryOutDto>> Handle(GetVourchersQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);
        var marathons = (await _unit.MarathonRepository
            .GetAllAsync(include: source => source
            .Include(a => a.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode))
            .Include(x => x.Vouchers)
            .ThenInclude(x => x.Promocodes))
            );
        var response = marathons.Adapt<IEnumerable<GetVourchersQueryOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return response;
    }
}
