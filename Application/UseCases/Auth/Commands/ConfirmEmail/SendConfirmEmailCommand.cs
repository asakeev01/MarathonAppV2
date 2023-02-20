using System;
using System.Net;
using Domain.Common.Contracts;
using MediatR;

namespace Core.UseCases.Auth.Commands.ConfirmEmail;

public class SendConfirmEmailCommand : IRequest<HttpStatusCode>
{
    public string Email { get; set; }
}

public class SendConfirmEmailHandler : IRequestHandler<SendConfirmEmailCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IEmailService _emailService;

    public SendConfirmEmailHandler(IUnitOfWork unit, IEmailService emailService)
    {
        _unit = unit;
        _emailService = emailService;
    }

    public async Task<HttpStatusCode> Handle(SendConfirmEmailCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.GetByEmailAsync(cmd.Email);
        var emailToken = await _unit.UserRepository.GenerateEmailConfirmationTokenAsync(identityUser);
        await _emailService.SendConfirmEmailAsync(identityUser.Email, emailToken);
        return HttpStatusCode.OK;
    }
}

