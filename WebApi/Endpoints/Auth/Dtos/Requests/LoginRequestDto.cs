using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests;

public class LoginRequestDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Password { get; set; }
}

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
        .EmailAddress()
        .MaximumLength(50);
    }
}


