using System;
using FluentValidation;

namespace WebApi.Endpoints.Applications.Dtos.Requests;

public class CreateApplicationViaMoneyRequestDto
{
    public int DistanceId { get; set; }
}

public class CreateApplicationViaMoneyRequestDtoValidator : AbstractValidator<CreateApplicationViaMoneyRequestDto>
{
    public CreateApplicationViaMoneyRequestDtoValidator()
    {
        RuleFor(x => x.DistanceId).NotEmpty();
        RuleFor(x => x.DistanceId).NotNull();
    }
}


