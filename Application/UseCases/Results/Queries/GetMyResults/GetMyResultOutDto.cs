using Core.Common.Bases;
using Domain.Entities.Results;
using Domain.Entities.Users.UserEnums;
using Domain.Entities.Users;
using Domain.Entities.Marathons;
using Domain.Entities.Applications;

namespace Core.UseCases.Results.Queries.GetMyResults;

public record GetMyResultOutDto : BaseDto<Result, GetMyResultOutDto>
{
    public int Id { get; set; }
    public string CategoryPlace { get; set; }
    public string GeneralPlace { get; set; }
    public int CategoryCount { get; set; }
    public int GeneralCount { get; set; }
    public string GunTime { get; set; }
    public string ChipTime { get; set; }
    public MarathonDto Marathon { get; set; }
    public UserDto User { get; set; }
    public ApplicationDto Application { get; set; }

    public record MarathonDto : BaseDto<Marathon, MarathonDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Name, y => y.MarathonTranslations.First().Name);
        }
    }

    public record UserDto : BaseDto<User, UserDto>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? FullName { get; set; }
        public string? FullNameR { get; set; }
        public int? Age { get; set; }
        public bool? Gender { get; set; }
        public CountriesEnum? Country { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.FullName, y => $"{y.Surname} {y.Name}")
                .Map(x => x.FullNameR, y => $"{y.Name} {y.Surname}");
        }
    }
    public record ApplicationDto : BaseDto<Application, ApplicationDto>
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string? DistanceAge { get; set; }
        public string? Distance { get; set; }
        public bool IsPWD { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.DistanceAge, y => y.DistanceAge == null ? "" : $"{y.DistanceAge.AgeFrom}-{y.DistanceAge.AgeTo}")
                .Map(x => x.Distance, y => y.Distance.Name)
                .Map(x => x.IsPWD, y => y.IsPWD);
        }
    }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.User.Age, y => y.Application.User.GetAge(y.Application.Marathon.Date))
            .Map(x => x.User.Age, y => y.Application.User.GetAge(y.Application.Marathon.Date))
            .Map(x => x.User, y => y.Application.User)
            .Map(x => x.Marathon, y => y.Application.Marathon)
            .Map(x => x.CategoryCount, y => y.Application.DistanceAgeId == null ? y.Application.Distance.Applications.Where(x => x.DistanceId == y.Application.DistanceId && x.IsPWD == true && y.Application.User.Gender == x.User.Gender).Count() : y.Application.Distance.Applications.Where(x => x.DistanceId == y.Application.DistanceId && x.DistanceAgeId == y.Application.DistanceAgeId).Count())
            .Map(x => x.GeneralCount, y => y.Application.DistanceAgeId == null ? y.Application.Distance.Applications.Where(x => x.DistanceId == y.Application.DistanceId && x.IsPWD == true).Count() : y.Application.Distance.Applications.Where(x => x.DistanceId == y.Application.DistanceId && x.IsPWD != true).Count())
            ;
    }
}
