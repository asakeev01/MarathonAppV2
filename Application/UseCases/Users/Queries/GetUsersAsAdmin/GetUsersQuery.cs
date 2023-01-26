using System;
using Domain.Common.Contracts;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Users.Queries.GetUsersAsAdmin;

public class GetUsersQuery : IRequest<QueryablePaging<GetUsersOutDto>>
{
    public GridifyQuery Query { get; set; }
}

public class GetUsersHandler : IRequestHandler<GetUsersQuery, QueryablePaging<GetUsersOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetUsersHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetUsersOutDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unit.UserRepository.GetAllAsync(include: source => source
        .Include(u=> u.Documents)
        .Include(u => u.Status)
        .Include(u => u.UserRoles).ThenInclude(r => r.Role)
        .Include(u => u.Applications.Where(x => x.Marathon.Date >= DateTime.Now).OrderBy(x => x.Marathon.Date))?.ThenInclude(x => x.Marathon)
            );
        var response = users.Adapt<IEnumerable<GetUsersOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return response;
    }
}

