using System;
using System.Net;
using Domain.Common.Contracts;
using MediatR;

namespace Core.UseCases.Auth.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<HttpStatusCode>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string NewPassword { get; set; }
}

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public ChangePasswordHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(ChangePasswordCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.GetByEmailAsync(cmd.Email);
        await _unit.UserRepository.CheckPasswordAsync(identityUser, cmd.Password);
        await _unit.UserRepository.ChangePasswordAsync(identityUser, cmd.Password, cmd.NewPassword);
        return HttpStatusCode.OK;
    }
}

