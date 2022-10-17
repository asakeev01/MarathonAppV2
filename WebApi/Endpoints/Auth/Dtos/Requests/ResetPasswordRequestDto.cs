using System;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests
{
    public class ResetPasswordRequestDto
    {
        public string PasswordToken { get; set; }

        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequestDto>
    {
        public ResetPasswordRequestValidator()
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

