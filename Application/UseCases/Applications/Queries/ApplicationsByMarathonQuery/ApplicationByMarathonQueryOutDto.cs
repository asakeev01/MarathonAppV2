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
    public int? DistanceId { get; set; }
    public int? DistanceForPWDId { get; set; }
    public UserDto User { get; set; }


    public record UserDto : BaseDto<User, UserDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public CountriesEnum Country { get; set; }
        public StatusesEnum CurrentStatus { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.CurrentStatus, y => y.Status.CurrentStatus);
        }
    }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.Voucher, y => y.Promocode.Voucher.Name);
    }
}
