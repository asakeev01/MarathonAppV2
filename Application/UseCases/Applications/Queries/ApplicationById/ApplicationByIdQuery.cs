using System;
using Domain.Common.Contracts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Applications.Queries.ApplicationById;

public class ApplicationByIdQuery : IRequest<ApplicationByIdQueryOutDto>
{
    public int ApplicationId { get; set; }
}

public class ApplicationByIdHandler : IRequestHandler<ApplicationByIdQuery, ApplicationByIdQueryOutDto>
{
    private readonly IUnitOfWork _unit;

    public ApplicationByIdHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<ApplicationByIdQueryOutDto> Handle(ApplicationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var application = await _unit.ApplicationRepository.FirstAsync(a => a.Id == request.ApplicationId && x.RemovalTime == null, include: source => source
            .Include(x => x.User).ThenInclude(x => x.Documents)
            .Include(x => x.User).ThenInclude(x => x.Status)
            .Include(x => x.Distance)
            .Include(x => x.DistanceForPWD)
            .Include(x => x.Promocode).ThenInclude(x => x.Voucher)
            );

        var result = application.Adapt<ApplicationByIdQueryOutDto>();

        return result;
    }
}


