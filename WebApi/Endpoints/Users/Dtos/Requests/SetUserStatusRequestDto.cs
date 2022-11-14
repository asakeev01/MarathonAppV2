using System;
using Domain.Entities.Users.UserEnums;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests;

public class SetUserStatusRequestDto
{
    public StatusesEnum NewStatus { get; set; }
    public CommentsEnum Comment { get; set; }
}

public class SetUserStatusRequestValidator : AbstractValidator<SetUserStatusRequestDto>
{
    public SetUserStatusRequestValidator()
    {
        RuleFor(x => x.NewStatus)
        .NotEmpty();

        RuleFor(x => x.Comment)
        .NotEmpty();
    }
}

