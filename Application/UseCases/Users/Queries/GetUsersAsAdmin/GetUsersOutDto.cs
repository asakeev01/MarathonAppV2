using System;
using Core.Common.Bases;
using Domain.Entities.Documents;
using Domain.Entities.Statuses;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;

namespace Core.UseCases.Users.Queries.GetUsersAsAdmin;

public record GetUsersOutDto : BaseDto<User, GetUsersOutDto>
{
    public string Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool? Gender { get; set; }
    public CountriesEnum? Country { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsDeleted { get; set; }
    public bool EmailConfirmed { get; set; }

    public DocumentDto Document { get; set; }

    public StatusDto Status { get; set; }

    public ICollection<UserRoleDto> UserRoles { get; set; }

    public record DocumentDto : BaseDto<Document, DocumentDto>
    {
        public long Id { get; set; }
        public string? FrontPassportPath { get; set; }
        public string? InsurancePath { get; set; }
        public string? DisabilityPath { get; set; }
    }

    public record StatusDto : BaseDto<Status, StatusDto>
    {
        public long Id { get; set; }
        public string CurrentStatus { get; set; }
    }

    public record UserRoleDto : BaseDto<UserRole, UserRoleDto>
    {
        public long Id { get; set; }
        public string Role { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Role, y => y.Role.Name);
        }
    }

}


