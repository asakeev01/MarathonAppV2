using System;
using Core.Common.Bases;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;

namespace Core.UseCases.Users.Queries.GetUserProfile
{
    public record GetUserProfileOutDto : BaseDto<User, GetUserProfileOutDto>
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool? Gender { get; set; }

        public TshirtEnum? Tshirt { get; set; }

        public CountriesEnum? Country { get; set; }

        public string PhoneNumber { get; set; }

        public string ExtraPhoneNumber { get; set; }
    }
}

