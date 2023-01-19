using System;
using System.Net;
using Domain.Common.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.ChangePassword;

public class SendPasswordResetTokenCommand : IRequest<HttpStatusCode>
{
    public string Email { get; set; }
}

public class SendPasswordResetTokenHandler : IRequestHandler<SendPasswordResetTokenCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IEmailService _emailService;

    public SendPasswordResetTokenHandler(IUnitOfWork unit, IEmailService emailService)
    {
        _unit = unit;
        _emailService = emailService;
    }

    public async Task<HttpStatusCode> Handle(SendPasswordResetTokenCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.GetByEmailAsync(cmd.Email);
        var passwordToken = await _unit.UserRepository.GeneratePasswordResetTokenAsync(identityUser);
        await _emailService.SendPasswordResetTokenAsync(identityUser.Email, passwordToken);
        return HttpStatusCode.OK;
    }
}

