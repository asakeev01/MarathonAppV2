using System;
using Domain.Common.Contracts;
using Mapster;
using MediatR;

namespace Core.UseCases.Users.Queries.GetProfile
{
    public class GetProfileQuery : IRequest<GetProfileOutDto>
    {
        public string? Email { get; set; }
    }

    public class GetProfileHandler : IRequestHandler<GetProfileQuery, GetProfileOutDto>
    {
        private readonly IUnitOfWork _unit;

        public GetProfileHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<GetProfileOutDto> Handle(GetProfileQuery request,
            CancellationToken cancellationToken)
        {
            var identityUser = _unit.UserRepository.GetByEmailAsync(request.Email);
            var profileDto = identityUser.Result.Adapt<GetProfileOutDto>();
            return profileDto;
        }
    }
}

