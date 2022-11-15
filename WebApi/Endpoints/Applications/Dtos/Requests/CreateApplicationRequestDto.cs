using FluentValidation;

namespace WebApi.Endpoints.Applications.Dtos.Requests;

public class CreateApplicationRequestDto
{
    public int DistanceId { get; set; }
    public string? Promocode { get; set; }
}

public class CreateApplicationRequestDtoValidator : AbstractValidator<CreateApplicationRequestDto>
{
    public CreateApplicationRequestDtoValidator()
    {
        RuleFor(x => x.DistanceId).NotEmpty();
        RuleFor(x => x.DistanceId).NotNull();
    }

}
