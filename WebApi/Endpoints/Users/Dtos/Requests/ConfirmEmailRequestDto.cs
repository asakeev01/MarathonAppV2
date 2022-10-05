using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests
{
    public class ConfirmEmailRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }

    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequestDto>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(50);
        }
    }
}

