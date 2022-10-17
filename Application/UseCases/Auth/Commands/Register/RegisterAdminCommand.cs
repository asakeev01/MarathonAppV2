using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Documents;
using Domain.Entities.Users;
using Domain.Entities.Users.Constants;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.Register
{
    public class RegisterAdminCommand : IRequest<HttpStatusCode>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork _unit;
        private readonly IEmailService _emailService;

        public RegisterAdminCommandHandler(IUnitOfWork unit, IEmailService emailService)
        {
            _unit = unit;
            _emailService = emailService;
        }

        public async Task<HttpStatusCode> Handle(RegisterAdminCommand cmd, CancellationToken cancellationToken)
        {
            await _unit.UserRepository.UserExistsAsync(cmd.Email);
            var identityUser = new User
            {
                Email = cmd.Email,
                UserName = cmd.Email
            };
            identityUser.Document = new Document();
            await _unit.UserRepository.CreateUserAsync(identityUser, cmd.Password);
            await _unit.UserRepository.AddToRoleAsync(identityUser, Roles.Admin);
            var emailToken = await _unit.UserRepository.GenerateEmailConfirmationTokenAsync(identityUser);
            await _emailService.SendConfirmEmailAsync(identityUser.Email, emailToken);
            return HttpStatusCode.Created;
        }
    }
}

