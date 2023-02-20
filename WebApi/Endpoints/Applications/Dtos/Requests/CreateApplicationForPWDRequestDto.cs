using FluentValidation;

namespace WebApi.Endpoints.Applications.Dtos.Requests;

public class CreateApplicationForPWDRequestDto
{
    public int DistanceId { get; set; }
}

public class CreateApplicationForPWDRequestDtoValidator : AbstractValidator<CreateApplicationForPWDRequestDto>
{
    public CreateApplicationForPWDRequestDtoValidator()
    {
        RuleFor(x => x.DistanceId).NotEmpty();
        RuleFor(x => x.DistanceId).NotNull();
    }

}
