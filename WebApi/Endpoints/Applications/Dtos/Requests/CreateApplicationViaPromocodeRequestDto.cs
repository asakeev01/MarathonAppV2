using FluentValidation;

namespace WebApi.Endpoints.Applications.Dtos.Requests;

public class CreateApplicationViaPromocodeRequestDto
{
    public int DistanceId { get; set; }
    public string Promocode { get; set; }
}

public class CreateApplicationRequestDtoValidator : AbstractValidator<CreateApplicationViaPromocodeRequestDto>
{
    public CreateApplicationRequestDtoValidator()
    {
        RuleFor(x => x.DistanceId).NotEmpty();
        RuleFor(x => x.DistanceId).NotNull();
        RuleFor(x => x.Promocode).NotEmpty();
        RuleFor(x => x.Promocode).NotNull();
    }

}
