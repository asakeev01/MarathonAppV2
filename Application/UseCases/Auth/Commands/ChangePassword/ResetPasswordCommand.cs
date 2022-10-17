﻿using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.ChangePassword
{
    public class ResetPasswordCommand : IRequest<HttpStatusCode>
    {
        public string PasswordToken { get; set; }

        public string Email { get; set; }

        public string NewPassword { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork _unit;
        private readonly IEmailService _emailService;

        public ResetPasswordCommandHandler(IUnitOfWork unit, IEmailService emailService)
        {
            _unit = unit;
            _emailService = emailService;
        }

        public async Task<HttpStatusCode> Handle(ResetPasswordCommand cmd, CancellationToken cancellationToken)
        {
            var identityUser = await _unit.UserRepository.GetByEmailAsync(cmd.Email);
            var normalToken = _emailService.WebDecodeToken(cmd.PasswordToken);
            await _unit.UserRepository.ResetPasswordAsync(identityUser, normalToken, cmd.NewPassword);
            return HttpStatusCode.OK;
        }
    }
}

