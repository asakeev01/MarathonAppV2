using Domain.Common.Constants;
using FluentValidation;

namespace WebApi.Endpoints.Distances.Dtos.Requests
{
   public class CreateDistanceCategoryRequestDto
    {
        public ICollection<TranslationDistanceCategoryDto> Translations { get; set; }

        public class TranslationDistanceCategoryDto
        {
            public string Name { get; set; }
            public int LanguageId { get; set; }
        }

        }
    public class CreateDistanceCategoryRequestValidator : AbstractValidator<CreateDistanceCategoryRequestDto>
    {
        public CreateDistanceCategoryRequestValidator()
        {
            RuleFor(x => x.Translations).Must(x => x.Select((o) => o.LanguageId).OrderBy(x => x).ToArray().SequenceEqual(AppConstants.SupportedLanguagesIds)).WithMessage($"Wrong LanguageIds in Translations. Ids must be {string.Join(", ", AppConstants.SupportedLanguagesIds)}");

            RuleForEach(x => x.Translations).ChildRules(translations =>
            {
                translations.RuleFor(x => x.Name).NotEmpty();
            });
        }
    }
}
