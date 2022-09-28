using Core.Common.Bases;
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

        public int Id { get; set; }
        public string NameRu { get; set; }
        public string TextRu { get; set; }
        public string PlaceRu { get; set; }
        public string NameEn { get; set; }
        public string TextEn { get; set; }
        public string PlaceEn { get; set; }
        public string NameKg { get; set; }
        public string TextKg { get; set; }
        public string PlaceKg { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDateAcceptingApplications { get; set; }
        public DateTime EndDateAcceptingApplications { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DistanceDto> Distances { get; set; }

        //public override void AddCustomMappings()
        //{
        //    SetCustomMappings()
        //        .Map(x => x.Name, y => y.MarathonTranslations.First().Name)
        //        .Map(x => x.Text, y => y.MarathonTranslations.First().Text)
        //        .Map(x => x.Place, y => y.MarathonTranslations.First().Place);
        //}

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
            public int MarathonId { get; set; }
            public int DistanceCategoryId { get; set; }
            public virtual ICollection<DistancePriceDto> DistancePrices { get; set; }
            public virtual ICollection<DistanceAgeDto> DistanceAges { get; set; }

            //public override void AddCustomMappings()
            //{
            //    SetCustomMappings()
            //        .Map(x => x.Name, y => y.DistanceCategory.DistanceCategoryTranslations.First().Name);
            //}

            public class DistancePriceDto
            {
                public int Id { get; set; }
                public DateTime DateStart { get; set; }
                public DateTime DateEnd { get; set; }
                public double Price { get; set; }
                public int DistanceId { get; set; }
            }

            public class DistanceAgeDto
            {
                public int Id { get; set; }
                public int? AgeFrom { get; set; }
                public int? AgeTo { get; set; }
                public int DistanceId { get; set; }
            }
        }

    }
}