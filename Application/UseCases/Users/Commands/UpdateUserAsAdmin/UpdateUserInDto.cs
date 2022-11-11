using System;
using Core.Common.Bases;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;

namespace Core.UseCases.Users.Commands.UpdateUserAsAdmin;

public record UpdateUserInDto : BaseDto<UpdateUserInDto, User>
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public bool? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ExtraPhoneNumber { get; set; }
    public bool IsDisable { get; set; }
    public bool IsDeleted { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfConfirmation { get; set; }
    public TshirtEnum? Tshirt { get; set; }
    public CountriesEnum? Country { get; set; }
}

