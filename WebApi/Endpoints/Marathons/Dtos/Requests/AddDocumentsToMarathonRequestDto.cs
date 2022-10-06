using FluentValidation;

namespace WebApi.Endpoints.Marathons.Dtos.Requests;

public class AddDocumentsToMarathonRequestDto
{
    public ICollection<IFormFile> Documents { get; set; }
}

public class AddDocumentsToMarathonRequestDtoValidator : AbstractValidator<AddDocumentsToMarathonRequestDto>
{
    public AddDocumentsToMarathonRequestDtoValidator()
    {
        RuleForEach(x => x.Documents).ChildRules(documents =>
        {
            documents.RuleFor(x => x.Length).LessThanOrEqualTo(20 * 1024 * 1024)
            .WithMessage("File size is larger than allowed");
        });
    }

}
