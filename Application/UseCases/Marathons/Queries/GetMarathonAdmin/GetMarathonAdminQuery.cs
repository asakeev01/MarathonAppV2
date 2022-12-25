using Domain.Common.Contracts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Marathons.Queries.GetMarathonAdmin;

public class GetMarathonAdminQuery : IRequest<GetMarathonAdminOutDto>
{
    public int MarathonId { get; set; }
}

public class GetMarathonAdminHandler : IRequestHandler<GetMarathonAdminQuery, GetMarathonAdminOutDto>
{
    private readonly IUnitOfWork _unit;

    public GetMarathonAdminHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<GetMarathonAdminOutDto> Handle(GetMarathonAdminQuery request,
        CancellationToken cancellationToken)
    {
        var marathon = await _unit.MarathonRepository
            .FirstToAsync<GetMarathonAdminOutDto>(x => x.Id == request.MarathonId,
            include: source => source
            .Include(a => a.DistancesForPWD)
            .Include(a => a.MarathonTranslations).ThenInclude(a => a.Logo)
            .Include(a => a.Documents)
            .Include(a => a.Partners).ThenInclude(a => a.Translations)
            .Include(a => a.Partners).ThenInclude(a => a.PartnerCompanies).ThenInclude(a => a.Logo)
            .Include(a => a.Distances).ThenInclude(a => a.DistancePrices)
            .Include(a => a.Distances).ThenInclude(a => a.DistanceAges));
                
        return marathon;
    }
}
