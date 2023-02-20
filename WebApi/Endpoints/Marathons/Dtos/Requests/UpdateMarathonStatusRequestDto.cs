using Domain.Common.Constants;
using FluentValidation;

namespace WebApi.Endpoints.Marathons.Dtos.Requests;

public class UpdateMarathonStatusRequestDto
{
    public int MarathonId { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateMarathonStatusRequestDtoValidator : AbstractValidator<UpdateMarathonStatusRequestDto>
{
    public UpdateMarathonStatusRequestDtoValidator()
    {
        RuleFor(x => x.MarathonId).NotEmpty();
        RuleFor(x => x.IsActive).NotNull();
    }
}