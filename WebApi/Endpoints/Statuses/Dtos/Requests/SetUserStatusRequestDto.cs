using System;
using Domain.Entities.Statuses.StatusEnums;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests;

public class SetUserStatusRequestDto
{
    public StatusesEnum NewStatus { get; set; }
    public ICollection<long>? CommentsId { get; set; }
}

public class SetUserStatusRequestValidator : AbstractValidator<SetUserStatusRequestDto>
{
    public SetUserStatusRequestValidator()
    {
    }
}

