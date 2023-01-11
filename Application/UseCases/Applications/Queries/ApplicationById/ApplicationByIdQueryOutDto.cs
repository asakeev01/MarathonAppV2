﻿using Core.Common.Bases;
using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Documents;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;

namespace Core.UseCases.Applications.Queries.ApplicationById;

public record ApplicationByIdQueryOutDto : BaseDto<Application, ApplicationByIdQueryOutDto>
{
    public int Id { get; set; }
    public string? Magnet { get; set; }
    public DateTime Date { get; set; }
    public int Number { get; set; }
    public StartKitEnum StarterKit { get; set; }
    public PaymentMethodEnum Payment { get; set; }
    public decimal? Price { get; set; }
    public decimal? Paid { get; set; }
    public string StarterKitCode { get; set; }
    public string? FullNameRecipient { get; set; }
    public DateTime? DateOfIssue { get; set; }
    public string? Distance { get; set; }
    public string? DistanceForPWD { get; set; }
    public string? Voucher { get; set; }
    public UserDto User { get; set; }

    public record UserDto : BaseDto<User, UserDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtraPhoneNumber { get; set; }
        public bool IsDisable { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateOfConfirmation { get; set; }
        public TshirtEnum? Tshirt { get; set; }
        public CountriesEnum? Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public StatusesEnum CurrentStatus { get; set; }
        public DocumentDto Document { get; set; }

        public record DocumentDto : BaseDto<Document, DocumentDto>
        {
            public long Id { get; set; }
            public string? FrontPassportPath { get; set; }
            public string? InsurancePath { get; set; }
            public string? DisabilityPath { get; set; }
            public string? BackPassportPath { get; set; }
            public string? BackInsurancePath { get; set; }
            public string? BackDisabilityPath { get; set; }
        }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.CurrentStatus, y => y.Status.CurrentStatus)
                .Map(x => x.Document, y => y.Documents.Where(x => x.IsArchived == false).FirstOrDefault());
        }
    }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
            .Map(x => x.Distance, y => y.Distance.Name)
            .Map(x => x.DistanceForPWD, y => y.DistanceForPWD.Name)
            .Map(x => x.Voucher, y => y.Promocode.Voucher.Name);
    }
}