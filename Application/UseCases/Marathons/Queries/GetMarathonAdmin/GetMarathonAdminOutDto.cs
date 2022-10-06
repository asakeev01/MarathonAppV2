using Core.Common.Bases;
using Domain.Entities.Marathons;
using Domain.Entities.SavedFiles;
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
        public ICollection<TranslationMarathonDto> Translations { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DistanceDto> Distances { get; set; }
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
                .Map(x => x.Logo, y => y.Logo.Path)
                .Map(x => x.Translations, y => y.MarathonTranslations)
                .Map(x => x.Partners, y => y.Partners);
        }

        public record PartnerDto : BaseDto<Partner, PartnerDto>
        {
            public int Id { get; set; }
            public ICollection<PartnerTranslationDto> Translations { get; set; }
            public ICollection<LogosDto> Logos { get; set; }
            public override void AddCustomMappings()
            {
                SetCustomMappings()
                    .Map(x => x.Translations, y => y.Translations)
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
        public class PartnerTranslationDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LanguageId { get; set; }
        }
        public class TranslationMarathonDto
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
