using System;
using FluentValidation;

namespace WebApi.Endpoints.Applications.Dtos.Requests;

public class CreatePaymentRequestDto
{
    public int DistanceId { get; set; }
}

public class CreatePaymentRequestDtoValidator : AbstractValidator<CreatePaymentRequestDto>
{
    public CreatePaymentRequestDtoValidator()
    {
        RuleFor(x => x.DistanceId).NotEmpty();
        RuleFor(x => x.DistanceId).NotNull();
    }
}


