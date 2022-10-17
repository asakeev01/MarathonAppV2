using System;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests
{
    public class ChangePasswordRequestDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequestDto>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(50);

            RuleFor(x => x.NewPassword)
            .Equal(x => x.ConfirmPassword)
            .WithMessage("Passwords do not match.");
        }
    }
}

