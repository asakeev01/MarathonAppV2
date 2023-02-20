using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Documents;
using Domain.Entities.Users;
using Domain.Entities.Users.Constants;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.Register;

public class RegisterUserCommand : IRequest<HttpStatusCode>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public RegisterUserHandler(IUnitOfWork unit, IUserService userService, IEmailService emailService)
    {
        _unit = unit;
        _userService = userService;
        _emailService = emailService;
    }

    public async Task<HttpStatusCode> Handle(RegisterUserCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = _userService.CreateUser(cmd.Email);
        await _unit.UserRepository.CreateUserAsync(identityUser, cmd.Password);
        await _unit.UserRepository.AddToRoleAsync(identityUser, Roles.User);
        var emailToken = await _unit.UserRepository.GenerateEmailConfirmationTokenAsync(identityUser);
        await _emailService.SendConfirmEmailAsync(identityUser.Email, emailToken);
        return HttpStatusCode.Created;
    }
}


