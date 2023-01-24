using System;
using Domain.Common.Contracts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Applications.Queries.ApplicationByStarterKitCodeQuery;

public class ApplicationByStarterKitCodeQuery : IRequest<ApplicationByStarterKitCodeQueryOutDto>
{
    public int MarathonId { get; set; }
    public string StarterKitCode { get; set; }
}

public class ApplicationByStarterKitCodeHandler : IRequestHandler<ApplicationByStarterKitCodeQuery, ApplicationByStarterKitCodeQueryOutDto>
{
    private readonly IUnitOfWork _unit;

    public ApplicationByStarterKitCodeHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<ApplicationByStarterKitCodeQueryOutDto> Handle(ApplicationByStarterKitCodeQuery request,
        CancellationToken cancellationToken)
    {
        var application = await _unit.ApplicationRepository.FirstAsync(a => a.StarterKitCode == request.StarterKitCode && a.MarathonId == request.MarathonId && a.RemovalTime == null, include: source => source
            .Include(x => x.User).ThenInclude(x => x.Documents)
            .Include(x => x.User).ThenInclude(x => x.Status)
            .Include(x => x.Distance)
            .Include(x => x.DistanceAge)
            .Include(x => x.Marathon)
            );

        var result = application.Adapt<ApplicationByStarterKitCodeQueryOutDto>();

        return result;
    }
}


