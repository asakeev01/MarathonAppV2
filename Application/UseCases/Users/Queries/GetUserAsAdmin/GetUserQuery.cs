using System;
using Domain.Common.Contracts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Users.Queries.GetUserAsAdmin
{
    public class GetUserQuery : IRequest<GetUserOutDto>
    {
        public long? Id { get; set; }
    }

    public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserOutDto>
    {
        private readonly IUnitOfWork _unit;

        public GetUserHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<GetUserOutDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unit.UserRepository.FirstAsync(u => u.Id == request.Id, include: source => source
            .Include(u => u.Documents)
            .Include(u => u.Status).ThenInclude(s => s.StatusComments).ThenInclude(c => c.Comment)
            .Include(u => u.UserRoles).ThenInclude(r => r.Role));
            var response = user.Adapt<GetUserOutDto>();
            return response;
        }
    }
}

