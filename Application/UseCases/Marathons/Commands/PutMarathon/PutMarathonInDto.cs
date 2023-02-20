using Core.Common.Bases;
using Domain.Entities.Marathons;
using Microsoft.AspNetCore.Http;

namespace Core.UseCases.Marathons.Commands.PutMarathon;

public record PutMarathonInDto : BaseDto<PutMarathonInDto, Marathon>
{
    public int Id { get; set; }
    public ICollection<TranslationDto> Translations { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartDateAcceptingApplications { get; set; }
    public DateTime EndDateAcceptingApplications { get; set; }
    public bool IsActive { get; set; }
    public ICollection<DistanceDto> Distances { get; set; }
    public ICollection<PartnersDto> Partners { get; set; }

    public class TranslationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
        public int LanguageId { get; set; }

    }

    public class PartnersDto
    {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public ICollection<PartnerTrasnlationDto> Translations { get; set; }
        public ICollection<CompanyDto> PartnerCompanies { get; set; }

        public class CompanyDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
        }

        public class PartnerTrasnlationDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int LanguageId { get; set; }
        }
    }

    public class DistanceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartNumbersFrom { get; set; }
        public int StartNumbersTo { get; set; }
        public virtual ICollection<DistancePriceDto> DistancePrices { get; set; }
        public virtual ICollection<DistanceAgeDto> DistanceAges { get; set; }

        public class DistancePriceDto
        {
            public int Id { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime DateEnd { get; set; }
            public double Price { get; set; }
        }

        public class DistanceAgeDto
        {
            public int Id { get; set; }
            public bool Gender { get; set; }
            public int? AgeFrom { get; set; }
            public int? AgeTo { get; set; }
        }

    }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.MarathonTranslations, x => x.Translations);
    }
}

public class UpdateMarathonLogos
{
    public int LanguageId { get; set; }
    public IFormFile Logo { get; set; }
}

public class UpdatePartnerCompanyLogo
{
    public int SerialNumber { get; set; }
    public string Name { get; set; }
    public IFormFile Logo { get; set; }
}