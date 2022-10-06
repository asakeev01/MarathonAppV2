using Core.Common.Bases;
using Domain.Entities.Distances;
using Domain.Entities.Marathons;
using Domain.Entities.SavedFiles;

namespace Core.UseCases.Marathons.Queries.GetMarathon;

public record GetMarathonOutDto : BaseDto<Marathon, GetMarathonOutDto>
{

    public int Id { get; set; }
    public string Logo { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public string Place { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartDateAcceptingApplications { get; set; }
    public DateTime EndDateAcceptingApplications { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<DistanceDto> Distances { get; set; }
    public ICollection<PartnerDto> Partners { get; set; }
    public ICollection<DocumentDto> Documents { get; set; }

    public record DocumentDto : BaseDto<SavedFile, DocumentDto>
    {
        public int Id { get; set; }
        public string Document { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Document, y => y.Path);
        }
    }
    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.Name, y => y.MarathonTranslations.First().Name)
            .Map(x => x.Text, y => y.MarathonTranslations.First().Text)
            .Map(x => x.Logo, y => y.Logo.Path)
            .Map(x => x.Place, y => y.MarathonTranslations.First().Place);
    }
    public record PartnerDto : BaseDto<Partner, PartnerDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<LogosDto> Logos { get; set; }
        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Name, y => y.Translations.First().Name)
                .Map(x => x.Logos, y => y.Logos);
        }
    }

    public record LogosDto : BaseDto<SavedFile, LogosDto>
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Logo, y => y.Path);
        }
    }


    public record DistanceDto : BaseDto<Distance, DistanceDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan PassingLimit { get; set; }
        public int AgeFrom { get; set; }
        public int NumberOfParticipants { get; set; }
        public int RegistredParticipants { get; set; }
        public int RemainingPlaces { get; set; }
        public bool MedicalCertificate { get; set; }
        public int MarathonId { get; set; }
        public int DistanceCategoryId { get; set; }
        public virtual ICollection<DistancePriceDto> DistancePrices { get; set; }
        public virtual ICollection<DistanceAgeDto> DistanceAges { get; set; }

        public record DistancePriceDto : BaseDto<DistancePrice, DistancePriceDto>
        {
            public int Id { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime DateEnd { get; set; }
            public double Price { get; set; }
            public int DistanceId { get; set; }
        }

        public record DistanceAgeDto : BaseDto<DistanceAge, DistanceAgeDto>
        {
            public int Id { get; set; }
            public int? AgeFrom { get; set; }
            public int? AgeTo { get; set; }
            public int DistanceId { get; set; }
        }
    }
}