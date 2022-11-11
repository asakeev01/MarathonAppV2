using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using Mapster;
using MediatR;

namespace Core.UseCases.Users.Commands.UpdateUserAsAdmin;

public class UpdateUserCommand : IRequest<HttpStatusCode>
{
    public UpdateUserInDto UserDto { get; set; }
}

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IUserService _userService;

    public UpdateUserHandler(IUnitOfWork unit, IUserService userService)
    {
        _unit = unit;
        _userService = userService;
    }

    public async Task<HttpStatusCode> Handle(UpdateUserCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.GetByIdAsync(cmd.UserDto.Id);
        cmd.UserDto.Adapt(identityUser);
        await _unit.UserRepository.UpdateAsync(identityUser);

        return HttpStatusCode.OK;
    }
}

