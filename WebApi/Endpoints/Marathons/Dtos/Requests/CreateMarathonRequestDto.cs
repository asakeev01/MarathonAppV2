﻿using Domain.Common.Constants;
using FluentValidation;

namespace WebApi.Endpoints.Marathons.Dtos.Requests;

public class CreateMarathonRequestDto
{
    public ICollection<TranslationDto> Translations { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartDateAcceptingApplications { get; set; }
    public DateTime EndDateAcceptingApplications { get; set; }
    public bool IsActive { get; set; }
    public ICollection<DistanceDto> Distances { get; set; }
    public ICollection<PartnersDto> Partners { get; set; }
    public ICollection<IFormFile> Documents { get; set; }

    public class TranslationDto
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
        public int LanguageId { get; set; }
        public IFormFile Logo { get; set; }
    }

    public class PartnersDto
    {
        public int SerialNumber { get; set; }
        public ICollection<PartnerTrasnlationDto> Translations { get; set; }
        public ICollection<CompanyDto> PartnerCompanies { get; set; }
        public class CompanyDto
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public IFormFile Logo { get; set; }
        }

        public class PartnerTrasnlationDto
        {
            public string Name { get; set; }
            public int LanguageId { get; set; }
        }

    }

    public class DistanceDto
    {
        public string Name { get; set; }
        public int StartNumbersFrom { get; set; }
        public int StartNumbersTo { get; set; }
        public virtual ICollection<DistancePriceDto> DistancePrices { get; set; }
        public virtual ICollection<DistanceAgeDto> DistanceAges { get; set; }

        public class DistancePriceDto
        {
            public DateTime DateStart { get; set; }
            public DateTime DateEnd { get; set; }
            public double Price { get; set; }
        }

        public class DistanceAgeDto
        {
            public bool Gender { get; set; }
            public int? AgeFrom { get; set; }
            public int? AgeTo { get; set; }
        }
    }
}
 
public class CreateMarathonRequestValidator : AbstractValidator<CreateMarathonRequestDto>
{
    public CreateMarathonRequestValidator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.StartDateAcceptingApplications).NotEmpty();
        RuleFor(x => x.IsActive).NotEmpty();
        RuleFor(x => x.Translations).Must(x => x.Select((o) => o.LanguageId).OrderBy(x => x).ToArray().SequenceEqual(AppConstants.SupportedLanguagesIds)).WithMessage($"Wrong LanguageIds in Translations. Ids must be {string.Join(", ", AppConstants.SupportedLanguagesIds)}");

        RuleForEach(x => x.Documents).ChildRules(document =>
        {
            document.RuleFor(x => x.Length).LessThanOrEqualTo(20 * 1024 * 1024)
            .WithMessage("File size is larger than allowed");
        }
        );

        RuleForEach(x => x.Partners).ChildRules(partners =>
        {
            partners.RuleFor(x => x.SerialNumber).NotNull();
            partners.RuleFor(x => x.Translations).Must(x => x.Select((o) => o.LanguageId).OrderBy(x => x).ToArray().SequenceEqual(AppConstants.SupportedLanguagesIds)).WithMessage($"Wrong LanguageIds in Translations. Ids must be {string.Join(", ", AppConstants.SupportedLanguagesIds)}");
            partners.RuleFor(x => x.PartnerCompanies.Select(y => y.Name).Distinct().ToList().Count).Equal(x => x.PartnerCompanies.Count).WithMessage("Names of companies must be unique."); ;
            partners.RuleForEach(x => x.Translations).ChildRules(partnerTranslation =>
            {
                partnerTranslation.RuleFor(x => x.Name).NotEmpty();
            });
        });

        RuleForEach(x => x.Translations).ChildRules(translations =>
        {
            translations.RuleFor(x => x.Name).NotEmpty();
            translations.RuleFor(x => x.Text).NotEmpty();
            translations.RuleFor(x => x.Place).NotEmpty();
            //translations.RuleFor(x => x.Logo.ContentType).Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
            //    .WithMessage("Only images are allowed");
            //translations.RuleFor(x => x.Logo.Length).NotNull().LessThanOrEqualTo(20 * 1024 * 1024)
            //    .WithMessage("File size is larger than allowed");
        });

        RuleForEach(x => x.Distances).ChildRules(distances =>
        {
            distances.RuleFor(x => x.Name).NotEmpty();
            distances.RuleFor(x => x.StartNumbersFrom).GreaterThan(-1);
            distances.RuleFor(x => x.StartNumbersTo).GreaterThan(x => x.StartNumbersFrom);


            distances.RuleForEach(x => x.DistancePrices).ChildRules(distancePrices =>
            {
                distancePrices.RuleFor(x => x.DateStart).NotEmpty();
                distancePrices.RuleFor(x => x.DateEnd).NotEmpty();
                distancePrices.RuleFor(x => x.Price).GreaterThan(0);
            });

            distances.RuleForEach(x => x.DistanceAges).ChildRules(distanceAges =>
            {
                distanceAges.RuleFor(x => x.AgeFrom).GreaterThan(-1);
                distanceAges.RuleFor(x => x.AgeTo).GreaterThan(-1);
            });

        });



    }
}
