using Core.Common.Bases;
using Domain.Entities.Marathons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Commands.PutMarathonDistances
{
    public record PutMarathonDistancesInDto : BaseDto<PutMarathonDistancesInDto, Marathon>
    {
        public int Id { get; set; }
        public ICollection<DistanceDto> Distances { get; set; }

        public class DistanceDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan PassingLimit { get; set; }
            public int AgeFrom { get; set; }
            public int NumberOfParticipants { get; set; }
            public int RegistredParticipants { get; set; }
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