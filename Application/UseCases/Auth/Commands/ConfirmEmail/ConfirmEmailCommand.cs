using System;
using System.Net;
using Domain.Common.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<HttpStatusCode>
{
    public string Email { get; set; }
    public string Token { get; set; }
}

public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IEmailService _emailService;

    public ConfirmEmailHandler(IUnitOfWork unit, IEmailService emailService)
    {
        _unit = unit;
        _emailService = emailService;
    }

    public async Task<HttpStatusCode> Handle(ConfirmEmailCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.GetByEmailAsync(cmd.Email);
        //var normalToken = _emailService.WebDecodeToken(cmd.Token);
        await _unit.UserRepository.ConfirmEmailAsync(identityUser, cmd.Token);
        return HttpStatusCode.OK;
    }
}

