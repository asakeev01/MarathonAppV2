using System.ComponentModel.DataAnnotations;
using System.Net;
using Core.Common.Contracts;
using Domain.Common.Contracts;
using Domain.Entities.Users.UserEnums;
using Domain.Services.Interfaces;
using Mapster;
using MediatR;

namespace Core.UseCases.Users.Commands.UpdateUserProfile;

public class UpdateUserProfileCommand : IRequest<HttpStatusCode>
{
    public UpdateUserProfileInDto UserDto { get; set; }
}

public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfileCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IUserService _userService;

    public UpdateUserProfileHandler(IUnitOfWork unit, IUserService userService)
    {
        _unit = unit;
        _userService = userService;
    }

    public async Task<HttpStatusCode> Handle(UpdateUserProfileCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.GetByIdAsync(cmd.UserDto.Id);
        cmd.UserDto.Adapt(identityUser);
        _userService.SetDateOfConfirmation(identityUser);
        await _unit.UserRepository.UpdateAsync(identityUser);

        return HttpStatusCode.OK;
    }
}