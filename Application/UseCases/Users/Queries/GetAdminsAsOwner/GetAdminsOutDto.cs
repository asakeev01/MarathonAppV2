using System;
using Core.Common.Bases;
using Domain.Entities.Users;

namespace Core.UseCases.Users.Queries.GetAdminsAsOwner;

public record GetAdminsOutDto : BaseDto<User, GetAdminsOutDto>
{
    public string Id { get; set; }
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public ICollection<UserRoleDto> UserRoles { get; set; }

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

