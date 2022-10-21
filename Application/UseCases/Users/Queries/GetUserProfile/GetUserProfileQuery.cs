using System;
using Domain.Common.Contracts;
using Mapster;
using MediatR;

namespace Core.UseCases.Users.Queries.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<GetUserProfileOutDto>
    {
        public string? Email { get; set; }
    }

    public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileOutDto>
    {
        private readonly IUnitOfWork _unit;

        public GetUserProfileHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<GetUserProfileOutDto> Handle(GetUserProfileQuery request,
            CancellationToken cancellationToken)
        {
            var identityUser = _unit.UserRepository.GetByEmailAsync(request.Email);
            var profileDto = identityUser.Result.Adapt<GetUserProfileOutDto>();
            return profileDto;
        }
    }
}

