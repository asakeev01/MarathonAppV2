using System;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Users.UserEnums;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests;

public class UpdateUserRequestDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public bool? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ExtraPhoneNumber { get; set; }
    public bool IsDisable { get; set; }
    public bool IsDeleted { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfConfirmation { get; set; }
    public TshirtEnum? Tshirt { get; set; }
    public CountriesEnum? Country { get; set; }
}

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequestDto>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty();

        RuleFor(x => x.Surname)
        .NotEmpty();

        RuleFor(x => x.DateOfBirth)
        .NotEmpty();

        RuleFor(x => x.Gender)
        .NotEmpty();

        RuleFor(x => x.Tshirt)
        .NotEmpty();

        RuleFor(x => x.Country)
        .NotEmpty();

        RuleFor(x => x.PhoneNumber)
        .NotEmpty();

        RuleFor(x => x.ExtraPhoneNumber)
        .NotEmpty();
    }
}

