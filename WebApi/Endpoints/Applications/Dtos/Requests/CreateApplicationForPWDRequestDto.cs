using FluentValidation;

namespace WebApi.Endpoints.Applications.Dtos.Requests;

public class CreateApplicationForPWDRequestDto
{
    public int DistanceForPWDId { get; set; }
}

public class CreateApplicationForPWDRequestDtoValidator : AbstractValidator<CreateApplicationForPWDRequestDto>
{
    public CreateApplicationForPWDRequestDtoValidator()
    {
        RuleFor(x => x.DistanceForPWDId).NotEmpty();
        RuleFor(x => x.DistanceForPWDId).NotNull();
    }

}
