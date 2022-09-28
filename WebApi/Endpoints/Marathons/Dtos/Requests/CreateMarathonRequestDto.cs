using FluentValidation;

namespace WebApi.Endpoints.Marathons.Dtos.Requests
{
    public class CreateMarathonRequestDto
    {
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

        public class DistanceDto
        {
            public TimeSpan StartTime { get; set; }
            public TimeSpan PassingLimit { get; set; }
            public int AgeFrom { get; set; }
            public int NumberOfParticipants { get; set; }
            public int RegistredParticipants { get; set; }
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
    public class CreateMarathonRequestValidator : AbstractValidator<CreateMarathonRequestDto>
    {
        public CreateMarathonRequestValidator()
        {
            RuleFor(x => x.NameRu).NotEmpty();
            RuleFor(x => x.TextRu).NotEmpty();
            RuleFor(x => x.PlaceRu).NotEmpty();
            RuleFor(x => x.NameEn).NotEmpty();
            RuleFor(x => x.TextEn).NotEmpty();
            RuleFor(x => x.PlaceEn).NotEmpty();
            RuleFor(x => x.NameKg).NotEmpty();
            RuleFor(x => x.TextKg).NotEmpty();
            RuleFor(x => x.PlaceKg).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.StartDateAcceptingApplications).NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty();
            RuleFor(x => x.Distances).NotEmpty();

            RuleForEach(x => x.Distances).ChildRules(orders =>
            {
                orders.RuleFor(x => x.StartTime).NotEmpty();
                orders.RuleFor(x => x.PassingLimit).NotEmpty();
                orders.RuleFor(x => x.AgeFrom).GreaterThan(-1);
                orders.RuleFor(x => x.NumberOfParticipants).GreaterThan(0);
                orders.RuleFor(x => x.RegistredParticipants).GreaterThan(0);
                orders.RuleFor(x => x.MedicalCertificate).NotEmpty();
                orders.RuleFor(x => x.DistanceCategoryId).NotEmpty();

                orders.RuleForEach(x => x.DistancePrices).ChildRules(distancePrices =>
                {
                    distancePrices.RuleFor(x => x.DateStart).NotEmpty();
                    distancePrices.RuleFor(x => x.DateEnd).NotEmpty();
                    distancePrices.RuleFor(x => x.Price).GreaterThan(0);
                });

                orders.RuleForEach(x => x.DistanceAges).ChildRules(distanceAges =>
                {
                    distanceAges.RuleFor(x => x.AgeFrom).GreaterThan(-1);
                    distanceAges.RuleFor(x => x.AgeTo).GreaterThan(-1);
                });

            });



        }
    }
}
