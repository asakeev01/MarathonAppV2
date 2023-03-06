using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Documents;
using Domain.Entities.Users;
using Domain.Entities.Users.Constants;
using Domain.Entities.Users.UserEnums;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.Register;

public class RegisterAdminCommand : IRequest<HttpStatusCode>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public RolesEnum Role { get; set; }
}

public class RegisterAdminHandler : IRequestHandler<RegisterAdminCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;

    public RegisterAdminHandler(IUnitOfWork unit, IUserService userService, IEmailService emailService)
    {
        _unit = unit;
        _userService = userService;
        _emailService = emailService;
    }

    public async Task<HttpStatusCode> Handle(RegisterAdminCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = _userService.CreateUser(cmd.Email);
        await _unit.UserRepository.CreateUserAsync(identityUser, cmd.Password);
        await _unit.UserRepository.AddToRoleAsync(identityUser, cmd.Role.ToString());
        var emailToken = await _unit.UserRepository.GenerateEmailConfirmationTokenAsync(identityUser);
        await _emailService.SendConfirmEmailAdminAsync(identityUser.Email, emailToken, cmd.Email, cmd.Password);
        return HttpStatusCode.Created;
    }
}


