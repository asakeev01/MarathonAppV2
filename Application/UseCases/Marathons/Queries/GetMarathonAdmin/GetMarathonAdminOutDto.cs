using Core.Common.Bases;
using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Queries.GetMarathonAdmin
{
    public record GetMarathonAdminOutDto : BaseDto<Marathon, GetMarathonAdminOutDto>
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public ICollection<TranslationDto> Translations { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DistanceDto> Distances { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Logo, y => y.Logo.Path)
                .Map(x => x.Translations, y => y.MarathonTranslations);
        }
        public class TranslationDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
            public string Place { get; set; }
            public int LanguageId { get; set; }
        }

        public class DistanceDto
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
                public int? AgeFrom { get; set; }
                public int? AgeTo { get; set; }
            }
        }

    }
}
