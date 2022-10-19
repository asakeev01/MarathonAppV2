using System.ComponentModel.DataAnnotations;
using System.Net;
using Core.Common.Contracts;
using Domain.Common.Contracts;
using Domain.Entities.Users.UserEnums;
using Mapster;
using MediatR;

namespace Core.UseCases.Users.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<HttpStatusCode>
{
    public UpdateProfileInDto UserDto { get; set; }
}

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public UpdateProfileCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(UpdateProfileCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.GetByEmailAsync(cmd.UserDto.Email);
        cmd.UserDto.Adapt(identityUser);
        await _unit.UserRepository.UpdateAsync(identityUser);

        return HttpStatusCode.OK;
    }
}