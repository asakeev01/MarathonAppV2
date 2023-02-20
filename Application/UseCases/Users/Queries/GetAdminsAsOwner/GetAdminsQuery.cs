using System;
using Core.Common.Helpers;
using Domain.Common.Contracts;
using Domain.Entities.Users.Constants;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Users.Queries.GetAdminsAsOwner;

public class GetAdminsQuery : IRequest<QueryablePaging<GetAdminsOutDto>>
{
    public GridifyQuery Query { get; set; }
}

public class GetAdminsHandler : IRequestHandler<GetAdminsQuery, QueryablePaging<GetAdminsOutDto>>
{
    private readonly IUnitOfWork _unit;

    public GetAdminsHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<QueryablePaging<GetAdminsOutDto>> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
    {
        var users = await _unit.UserRepository.FindByCondition(predicate: u => u.UserRoles.Any(r => Roles.GetAdminAndVolunteerRoles().Contains(r.Role.Name)), include: source => source
        .Include(u => u.UserRoles).ThenInclude(r => r.Role)).ToListAsync();
        var response = users.Adapt<IEnumerable<GetAdminsOutDto>>().AsQueryable().GridifyQueryable(request.Query);
        return response;
    }
}

