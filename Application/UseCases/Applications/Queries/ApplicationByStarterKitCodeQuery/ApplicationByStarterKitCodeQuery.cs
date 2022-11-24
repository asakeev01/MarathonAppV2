using System;
using Domain.Common.Contracts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Applications.Queries.ApplicationByStarterKitCodeQuery;

public class ApplicationByStarterKitCodeQuery : IRequest<ApplicationByStarterKitCodeQueryOutDto>
{
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
        var application = await _unit.ApplicationRepository.FirstAsync(a => a.StarterKitCode == request.StarterKitCode, include: source => source
            .Include(x => x.User).ThenInclude(x => x.Document)
            );

        var result = application.Adapt<ApplicationByStarterKitCodeQueryOutDto>();

        return result;
    }
}


