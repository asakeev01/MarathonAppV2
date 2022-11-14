using Core.Common.Bases;
using Domain.Entities.Marathons;

namespace Core.UseCases.Marathons.Commands.CreateMarathon;

public record CreateMarathonInDto : BaseDto<CreateMarathonInDto, Marathon>
{
    public ICollection<TranslationDto> Translations { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartDateAcceptingApplications { get; set; }
    public DateTime EndDateAcceptingApplications { get; set; }
    public bool IsActive { get; set; }
    public ICollection<DistanceDto> Distances { get; set; }
    public ICollection<DistanceForPWDDTO> DistancesForPWD { get; set; }
    public ICollection<PartnersDto> Partners { get; set; }

    public class PartnersDto
    {
        public int SerialNumber { get; set; }
        public ICollection<PartnerTrasnlationDto> Translations { get; set; }
    }

    public class PartnerTrasnlationDto
    {
        public string Name { get; set; }
        public int LanguageId { get; set; }
    }


    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.MarathonTranslations, y => y.Translations);
    }

    public class TranslationDto
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
        public int LanguageId { get; set; }
    }

    public class DistanceForPWDDTO
    {
        public string Name { get; set; }
        public int StartNumbersFrom { get; set; }
        public int StartNumbersTo { get; set; }
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
