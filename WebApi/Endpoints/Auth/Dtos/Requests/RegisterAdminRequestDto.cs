using System;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Users.UserEnums;
using FluentValidation;

namespace WebApi.Endpoints.Auth.Dtos.Requests;

public class RegisterAdminRequestDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Password { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string ConfirmPassword { get; set; }

    [Required]
    public RolesEnum Role { get; set; }
}

public class RegisterAdminRequestValidator : AbstractValidator<RegisterAdminRequestDto>
{
    public RegisterAdminRequestValidator()
    {
        RuleFor(x => x.Email)
        .EmailAddress()
        .MaximumLength(50);

        RuleFor(x => x.Password)
        .Equal(x => x.ConfirmPassword)
        .WithMessage("Passwords do not match.");
    }
}

