using FluentValidation;

namespace WebApi.Endpoints.Marathons.Dtos.Requests
{
    public class PutMarathonDistancesRequestDto
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
    public class PutMarathonDistancesRequestDtoValidator : AbstractValidator<PutMarathonDistancesRequestDto>
    {
        public PutMarathonDistancesRequestDtoValidator()
        {

            RuleForEach(x => x.Distances).ChildRules(distances =>
            {
                distances.RuleFor(x => x.Name).NotEmpty();
                distances.RuleFor(x => x.StartTime).NotEmpty();
                distances.RuleFor(x => x.PassingLimit).NotEmpty();
                distances.RuleFor(x => x.AgeFrom).GreaterThan(-1);
                distances.RuleFor(x => x.NumberOfParticipants).GreaterThan(0);
                distances.RuleFor(x => x.RegistredParticipants).GreaterThan(0);
                distances.RuleFor(x => x.MedicalCertificate).NotEmpty();

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
}
