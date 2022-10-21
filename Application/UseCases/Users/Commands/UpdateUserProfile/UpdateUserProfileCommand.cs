using System.ComponentModel.DataAnnotations;
using System.Net;
using Core.Common.Contracts;
using Domain.Common.Contracts;
using Domain.Entities.Users.UserEnums;
using Mapster;
using MediatR;

namespace Core.UseCases.Users.Commands.UpdateUserProfile;

public class UpdateUserProfileCommand : IRequest<HttpStatusCode>
{
    public UpdateUserProfileInDto UserDto { get; set; }
}

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public UpdateUserProfileCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(UpdateUserProfileCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.GetByEmailAsync(cmd.UserDto.Email);
        cmd.UserDto.Adapt(identityUser);
        await _unit.UserRepository.UpdateAsync(identityUser);

        return HttpStatusCode.OK;
    }
}