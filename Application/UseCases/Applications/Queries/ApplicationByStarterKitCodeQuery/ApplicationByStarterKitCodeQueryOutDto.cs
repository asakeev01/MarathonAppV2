﻿using System;
using Core.Common.Bases;
using Domain.Entities.Applications;
using Domain.Entities.Applications.ApplicationEnums;
using Domain.Entities.Documents;
using Domain.Entities.Statuses.StatusEnums;
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
    public int AgeFrom { get; set; }
    public int AgeTo { get; set; }
    public bool IsPWD { get; set; }

    public record UserDto : BaseDto<User, UserDto>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public TshirtEnum Tshirt { get; set; }
        public CountriesEnum Country { get; set; }
        public StatusesEnum CurrentStatus { get; set; }
        public string PhoneNumber { get; set; }

        public DocumentDto Document { get; set; }

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.CurrentStatus, y => y.Status.CurrentStatus)
                .Map(x => x.Document, y => y.Documents.Where(x => x.IsArchived == false).FirstOrDefault());
        }

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
    }

    public override void AddCustomMappings()
    {
        SetCustomMappings()
        .Map(x => x.Distance, y => y.Distance.Name)
        .Map(x => x.AgeFrom, y => y.DistanceAge.AgeFrom)
        .Map(x => x.AgeTo, y => y.DistanceAge.AgeTo)
        .Map(x => x.IsPWD, y => y.IsPWD)
        .Map(x => x.User.Age, y => y.User.GetAge(y.Marathon.Date));
    }
}