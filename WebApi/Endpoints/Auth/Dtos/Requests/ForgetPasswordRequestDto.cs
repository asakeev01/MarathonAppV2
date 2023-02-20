using System;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests;

public class ForgetPasswordRequestDto
{
    public string Email { get; set; }
}

public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequestDto>
{
    public ForgetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
        .EmailAddress()
        .MaximumLength(50);
    }
}


