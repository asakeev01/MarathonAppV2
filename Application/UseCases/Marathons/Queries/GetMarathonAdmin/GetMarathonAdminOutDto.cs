using Core.Common.Bases;
using Domain.Entities.Marathons;
using Domain.Entities.SavedFiles;

namespace Core.UseCases.Marathons.Queries.GetMarathonAdmin;

public record GetMarathonAdminOutDto : BaseDto<Marathon, GetMarathonAdminOutDto>
{
    public int Id { get; set; }
    public ICollection<TranslationMarathonDto> Translations { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartDateAcceptingApplications { get; set; }
    public DateTime EndDateAcceptingApplications { get; set; }
    public bool IsActive { get; set; }
    public ICollection<DistanceDto> Distances { get; set; }
    public IEnumerable<DistanceForPWDDTO> DistancesForPWD { get; set; }
    public ICollection<PartnerDto> Partners { get; set; }
    public ICollection<DocumentDto> Documents { get; set; }

    public record TranslationMarathonDto : BaseDto<MarathonTranslation, TranslationMarathonDto>
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
        public int LanguageId { get; set; }
        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Logo, y => y.Logo.Path);
        }
    }

    public record DocumentDto : BaseDto<SavedFile, DocumentDto>
    {
        public string Document { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Document, y => y.Path);
        }
    }

    public class DistanceForPWDDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartNumbersFrom { get; set; }
        public int StartNumbersTo { get; set; }
        public int AmountOfParticipants { get; set; }
        public int RemainingPlaces { get; set; }
        public int RegisteredParticipants { get; set; }
    }

    public record PartnerDto : BaseDto<Partner, PartnerDto>
    {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public ICollection<PartnerTranslationDto> Translations { get; set; }
        public ICollection<CompanyDto> PartnerCompanies { get; set; }

        public class PartnerTranslationDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LanguageId { get; set; }
        }

        public record CompanyDto : BaseDto<PartnerCompany, CompanyDto>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
            public string Logo { get; set; }
            public override void AddCustomMappings()
            {
                SetCustomMappings()
                .Map(x => x.Logo, y => y.Logo.Path);
            }
        }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Translations, y => y.Translations);
        }
    }

    public class DistanceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartNumbersFrom { get; set; }
        public int StartNumbersTo { get; set; }
        public int AmountOfParticipants { get; set; }
        public int RegisteredParticipants { get; set; }
        public int ReservedPlaces { get; set; }
        public int ActivatedReservedPlaces { get; set; }
        public int RemainingPlaces { get; set; }
        public ICollection<DistancePriceDto> DistancePrices { get; set; }
        public ICollection<DistanceAgeDto> DistanceAges { get; set; }

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
            .Map(x => x.Translations, y => y.MarathonTranslations)
            .Map(x => x.Partners, y => y.Partners);
    }
}
