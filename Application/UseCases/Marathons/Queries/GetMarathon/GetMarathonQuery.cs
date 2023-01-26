using Core.Common.Helpers;
using Domain.Common.Contracts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Marathons.Queries.GetMarathon;

public class GetMarathonQuery : IRequest<GetMarathonOutDto>
{
    public string LanguageCode { get; set; }
    public int MarathonId { get; set; }
}

public class GetMarathonHandler : IRequestHandler<GetMarathonQuery, GetMarathonOutDto>
{
    private readonly IUnitOfWork _unit;

    public GetMarathonHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<GetMarathonOutDto> Handle(GetMarathonQuery request,
        CancellationToken cancellationToken)
    {
        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);
        var marathon = await _unit.MarathonRepository
            .FirstAsync(x => x.Id == request.MarathonId, include: source => source
            .Include(a => a.Documents)
            .Include(a => a.Partners).ThenInclude(a => a.Translations.Where(t => t.Language.Code == request.LanguageCode))
            .Include(a => a.Partners).ThenInclude(a => a.PartnerCompanies).ThenInclude(a => a.Logo)
            .Include(a => a.Distances).ThenInclude(a => a.DistancePrices)
            .Include(a => a.Distances).ThenInclude(a => a.DistanceAges)
            .Include(a => a.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode)).ThenInclude(a => a.Logo)
            );

        var marathonDto = marathon.Adapt<GetMarathonOutDto>();
        return marathonDto;
    }
}
