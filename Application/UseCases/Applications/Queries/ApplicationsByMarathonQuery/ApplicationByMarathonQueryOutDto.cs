using Core.Common.Bases;
using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;

namespace Core.UseCases.Applications.Queries.ApplicationsByMarathonQuery;

public record ApplicationByMarathonQueryOutDto : BaseDto<Application, ApplicationByMarathonQueryOutDto>
{
    public int Id { get; set; }
    public string Magnet { get; set; }
    public int Number { get; set; }
    public StartKitEnum StarterKit { get; set; }
    public PaymentMethodEnum Payment { get; set; }
    public string? Voucher { get; set; }
    public string StarterKitCode { get; set; }
    public string? FullNameRecipient { get; set; }
    public DateTime? DateOfIssue { get; set; }
    public int? DistanceAgeId { get; set; }
    public string? Distance { get; set; }
    public int AgeFrom { get; set; }
    public int AgeTo { get; set; }
    public bool IsPWD { get; set; }
    public UserDto User { get; set; }


    public record UserDto : BaseDto<User, UserDto>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? FullName { get; set; }
        public string? FullNameR { get; set; }
        public string? Email { get; set; }
        public bool? Gender { get; set; }
        public bool? IsDisable { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public CountriesEnum? Country { get; set; }
        public StatusesEnum? CurrentStatus { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.CurrentStatus, y => y.Status.CurrentStatus)
                .Map(x => x.FullName, y => $"{y.Surname} {y.Name}")
                .Map(x => x.FullNameR, y => $"{y.Name} {y.Surname}");
        }
    }

    public override void AddCustomMappings()
    {
        SetCustomMappings()

            .Map(x => x.Voucher, y => y.Promocode.Voucher.Name)
            .Map(x => x.Distance, y => y.Distance.Name)
            .Map(x => x.AgeFrom, y => y.DistanceAge.AgeFrom)
            .Map(x => x.AgeTo, y => y.DistanceAge.AgeTo)
            .Map(x => x.IsPWD, y => y.IsPWD)
            .Map(x => x.User.Age, y => y.User.GetAge(y.Marathon.Date));
    }
}
