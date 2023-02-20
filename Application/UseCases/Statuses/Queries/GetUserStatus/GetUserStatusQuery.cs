using System;
using Domain.Common.Contracts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Statuses.Queries.GetUserStatus;

public class GetUserStatusQuery : IRequest<GetUserStatusOutDto>
{
    public long? Id { get; set; }
}

public class GetUserStatusHandler : IRequestHandler<GetUserStatusQuery, GetUserStatusOutDto>
{
    private readonly IUnitOfWork _unit;

    public GetUserStatusHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<GetUserStatusOutDto> Handle(GetUserStatusQuery request,
        CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.FirstAsync(predicate: x => x.Id == request.Id, include: source => source.Include(x => x.Status));
        var status = await _unit.StatusRepository.FirstAsync(s => s.Id == identityUser.Status.Id, include: source => source
            .Include(u => u.StatusComments).ThenInclude(sc => sc.Comment));
        var statusDto = status.Adapt<GetUserStatusOutDto>();
        return statusDto;
    }
}


