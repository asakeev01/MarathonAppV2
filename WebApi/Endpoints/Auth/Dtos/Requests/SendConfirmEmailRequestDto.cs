using System;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests
{
    public class SendConfirmEmailRequestDto
    {
        public string Email { get; set; }
    }

    public class SendConfirmEmailRequestValidator : AbstractValidator<SendConfirmEmailRequestDto>
    {
        public SendConfirmEmailRequestValidator()
        {
            RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(50);
        }
    }
}

