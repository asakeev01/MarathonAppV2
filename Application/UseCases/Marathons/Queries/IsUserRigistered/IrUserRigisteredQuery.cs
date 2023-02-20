using Core.Common.Helpers;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Marathons.Queries.IsUserRigistered;

public class IrUserRigisteredQuery : IRequest<IrUserRigisteredOutDto>
{
    public int MarathonId { get; set; }
    public int UserId { get; set; }
    public string LanguageCode { get; set; }
}

public class GetMarathonsHandler : IRequestHandler<IrUserRigisteredQuery, IrUserRigisteredOutDto>
{
    private readonly IUnitOfWork _unit;

    public GetMarathonsHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<IrUserRigisteredOutDto> Handle(IrUserRigisteredQuery request,
        CancellationToken cancellationToken)
    {

        request.LanguageCode = LanguageHelpers.CheckLanguageCode(request.LanguageCode);

        var application = await _unit.ApplicationRepository.FirstToAsync<IrUserRigisteredOutDto>(x => x.UserId == request.UserId && x.MarathonId == request.MarathonId && x.RemovalTime == null,
            include: source => source
            .Include(x => x.Marathon).ThenInclude(x => x.MarathonTranslations.Where(t => t.Language.Code == request.LanguageCode))
            .Include(x => x.Distance)
            .Include(x => x.Promocode).ThenInclude(x => x.Voucher)
            );

        return application;
    }
}
