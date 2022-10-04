using FluentValidation;

namespace WebApi.Endpoints.Marathons.Dtos.Requests
{
    public class AddLogoToMarathonRequestDto
    {
        public IFormFile logo { get; set; }
    }

    public class AddLogoToMarathonRequestValidator : AbstractValidator<AddLogoToMarathonRequestDto>
    {
        public AddLogoToMarathonRequestValidator()
        {
            RuleFor(x => x.logo.ContentType).Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("Only images are allowed");
            RuleFor(x => x.logo.Length).NotNull().LessThanOrEqualTo(20 * 1024 * 1024)
                .WithMessage("File size is larger than allowed");
        }

    }
}
