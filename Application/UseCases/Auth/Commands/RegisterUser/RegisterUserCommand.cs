using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Documents;
using Domain.Entities.Users;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Auth.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<HttpStatusCode>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork _unit;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(IUnitOfWork unit, IEmailService emailService)
        {
            _unit = unit;
            _emailService = emailService;
        }

        public async Task<HttpStatusCode> Handle(RegisterUserCommand cmd, CancellationToken cancellationToken)
        {
            await _unit.UserRepository.UserExistsAsync(cmd.Email);
            var identityUser = new User
            {
                Email = cmd.Email,
                UserName = cmd.Email
            };
            identityUser.Document = new Document();
            await _unit.UserRepository.CreateUserAsync(identityUser, cmd.Password);
            await _emailService.SendConfirmEmailAsync(identityUser);
            return HttpStatusCode.Created;
        }
    }
}

