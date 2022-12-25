using System;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Users;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests;

public class RegisterRequestDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Password { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string ConfirmPassword { get; set; }
}

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
        .EmailAddress()
        .MaximumLength(50);

        RuleFor(x => x.Password)
        .Equal(x => x.ConfirmPassword)
        .WithMessage("Passwords do not match.");
    }
}

