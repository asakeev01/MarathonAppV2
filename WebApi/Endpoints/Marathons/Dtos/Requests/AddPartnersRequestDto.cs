using Domain.Common.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WebApi.Common.Extensions.SwaggerServices;

namespace WebApi.Endpoints.Marathons.Dtos.Requests;

public class AddPartnersRequestDto
{
    public ICollection<TrasnlationDto> Translations { get; set; }
    [JsonIgnore]
    public ICollection<IFormFile> Logos { get; set; }
}

[ModelBinder(BinderType = typeof(MetadataValueModelBinder))]
public class TrasnlationDto
{
    public string Name { get; set; }
    public int LanguageId { get; set; }
}

public class AddPartnersRequestValidator : AbstractValidator<AddPartnersRequestDto>
{
    public AddPartnersRequestValidator()
    {

        RuleFor(x => x.Logos).NotNull();
        RuleForEach(x => x.Logos).ChildRules(logos =>
        {
            logos.RuleFor(x => x.ContentType).Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
            .WithMessage("Only images are allowed");
            logos.RuleFor(x => x.Length).LessThanOrEqualTo(20 * 1024 * 1024)
            .WithMessage("File size is larger than allowed");
        });
        RuleFor(x => x.Translations).NotNull();
        RuleFor(x => x.Translations).Must(x => x.Select((o) => o.LanguageId)
        .OrderBy(x => x).ToArray().SequenceEqual(AppConstants.SupportedLanguagesIds))
        .WithMessage($"Wrong LanguageIds in Translations. Ids must be {string.Join(", ", AppConstants.SupportedLanguagesIds)}");

        RuleForEach(x => x.Translations).ChildRules(translations =>
        {
            translations.RuleFor(x => x.Name).NotEmpty();
        });
    }
}
