using Domain.Common.Constants;
using FluentValidation;

namespace WebApi.Endpoints.Marathons.Dtos.Requests
{
    public class PutMarathonRequestDto
    {
        public int Id { get; set; }
        public ICollection<TranslationDto> Translations { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }
        public class TranslationDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
            public string Place { get; set; }
            public int LanguageId { get; set; }
        }
    }

    public class PutMarathonRequestDtoValidator: AbstractValidator<PutMarathonRequestDto>
    {
        public PutMarathonRequestDtoValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.StartDateAcceptingApplications).NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty();
            RuleFor(x => x.Translations).Must(x => x.Select((o) => o.LanguageId).OrderBy(x => x).ToArray().SequenceEqual(AppConstants.SupportedLanguagesIds)).WithMessage($"Wrong LanguageIds in Translations. Ids must be {string.Join(", ", AppConstants.SupportedLanguagesIds)}");

            RuleForEach(x => x.Translations).ChildRules(translations =>
                {
                    translations.RuleFor(x => x.Name).NotEmpty();
                    translations.RuleFor(x => x.Text).NotEmpty();
                    translations.RuleFor(x => x.Place).NotEmpty();
                });
        }
    }
}
