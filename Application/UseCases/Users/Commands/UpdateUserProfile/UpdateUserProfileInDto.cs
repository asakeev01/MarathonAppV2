﻿using System;
using Core.Common.Bases;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;

namespace Core.UseCases.Users.Commands.UpdateUserProfile;

public record UpdateUserProfileInDto : BaseDto<UpdateUserProfileInDto, User>
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public bool? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ExtraPhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public TshirtEnum? Tshirt { get; set; }
    public CountriesEnum? Country { get; set; }
}


