using System;
using Core.Common.Bases;
using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Documents;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;

namespace Core.UseCases.Applications.Queries.ApplicationByStarterKitCodeQuery;

public record ApplicationByStarterKitCodeQueryOutDto : BaseDto<Application, ApplicationByStarterKitCodeQueryOutDto>
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Magnet { get; set; }
    public StartKitEnum StarterKit { get; set; }
    public string? FullNameRecipient { get; set; }
    public DateTime? DateOfIssue { get; set; }
    public UserDto User { get; set; }
    public string? Distance { get; set; }
    public string? DistanceForPWD { get; set; }

    public record UserDto : BaseDto<User, UserDto>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public TshirtEnum Tshirt { get; set; }
        public CountriesEnum Country { get; set; }
        public string PhoneNumber { get; set; }

        public DocumentDto Document { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Age, y => y.GetAge())
                ;
        }

        public record DocumentDto : BaseDto<Document, DocumentDto>
        {
            public long Id { get; set; }
            public string? FrontPassportPath { get; set; }
            public string? InsurancePath { get; set; }
            public string? DisabilityPath { get; set; }
        }
    }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
        .Map(x => x.Distance, y => y.Distance.Name)
        .Map(x => x.DistanceForPWD, y => y.DistanceForPWD.Name);
    }
}

