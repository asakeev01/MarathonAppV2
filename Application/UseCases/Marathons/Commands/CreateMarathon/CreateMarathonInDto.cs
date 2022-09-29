﻿using Core.Common.Bases;
using Domain.Entities.Distances;
using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Commands.CreateMarathon
{
    public record CreateMarathonRequestInDto: BaseDto<CreateMarathonRequestInDto, Marathon>
    {
        public ICollection<TranslationDto> Translations { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DistanceDto> Distances { get; set; }

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

        public class DistanceDto
        {
            public TimeSpan StartTime { get; set; }
            public TimeSpan PassingLimit { get; set; }
            public int AgeFrom { get; set; }
            public int NumberOfParticipants { get; set; }
            public int RegistredParticipants { get; set; }
            public int RemainingPlaces { get; set; }
            public bool MedicalCertificate { get; set; }
            public int DistanceCategoryId { get; set; }
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
                public int? AgeFrom { get; set; }
                public int? AgeTo { get; set; }
            }
        }

    }
}