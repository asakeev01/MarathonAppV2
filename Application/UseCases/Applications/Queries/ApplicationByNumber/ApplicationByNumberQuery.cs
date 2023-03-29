using System;
using Domain.Common.Contracts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Applications.Queries.ApplicationByNumber;

public class ApplicationByNumberQuery : IRequest<ApplicationByNumberOutDto>
{
    public int MarathonId { get; set; }
    public int Number { get; set; }
}

public class ApplicationByNumberHandler : IRequestHandler<ApplicationByNumberQuery, ApplicationByNumberOutDto>
{
    private readonly IUnitOfWork _unit;

    public ApplicationByNumberHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<ApplicationByNumberOutDto> Handle(ApplicationByNumberQuery request,
        CancellationToken cancellationToken)
    {
        var application = await _unit.ApplicationRepository.FirstAsync(a => a.Number == request.Number && a.MarathonId == request.MarathonId && a.RemovalTime == null, include: source => source
            .Include(x => x.User).ThenInclude(x => x.Documents)
            .Include(x => x.User).ThenInclude(x => x.Status)
            .Include(x => x.Distance)
            .Include(x => x.DistanceAge)
            .Include(x => x.Marathon)
            );

        var result = application.Adapt<ApplicationByNumberOutDto>();

        return result;
    }
}


