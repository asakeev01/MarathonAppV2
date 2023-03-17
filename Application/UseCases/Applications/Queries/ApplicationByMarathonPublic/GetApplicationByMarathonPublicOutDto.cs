using Core.Common.Bases;
using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;

namespace Core.UseCases.Applications.Queries.ApplicationByMarathonPublic;

public record GetApplicationByMarathonPublicOutDto : BaseDto<Application, GetApplicationByMarathonPublicOutDto>
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string? DistanceAge { get; set; }
    public string? Distance { get; set; }
    public bool IsPWD { get; set; }
    public UserDto User { get; set; }


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

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.DistanceAge, y => y.DistanceAge == null ? "" : $"{y.DistanceAge.AgeFrom}-{y.DistanceAge.AgeTo}")
            .Map(x => x.Distance, y => y.Distance.Name)
            .Map(x => x.IsPWD, y => y.IsPWD)
            .Map(x => x.User.Age, y => y.User.GetAge(y.Marathon.Date));
    }
}
