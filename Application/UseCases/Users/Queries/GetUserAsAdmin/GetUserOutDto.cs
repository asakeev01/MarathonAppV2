using System;
using System.ComponentModel.DataAnnotations;
using Core.Common.Bases;
using Domain.Entities.Documents;
using Domain.Entities.Documents.DocumentEnums;
using Domain.Entities.Statuses;
using Domain.Entities.Users;
using Domain.Entities.Users.UserEnums;

namespace Core.UseCases.Users.Queries.GetUserAsAdmin
{
    public record GetUserOutDto : BaseDto<User, GetUserOutDto>
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public TshirtEnum? Tshirt { get; set; }
        public CountriesEnum? Country { get; set; }
        public string? ExtraPhoneNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfConfirmation { get; set; }
        public bool IsDisable { get; set; }
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
            public string? BackPassportPath { get; set; }
            public string? BackInsurancePath { get; set; }
            public string? BackDisabilityPath { get; set; }
        }

        public record StatusDto : BaseDto<Status, StatusDto>
        {
            public long Id { get; set; }
            public string CurrentStatus { get; set; }
            public ICollection<CommentDto> Comments { get; set; }

            public override void AddCustomMappings()
            {
                SetCustomMappings()
                    .Map(x => x.Comments, y => y.StatusComments.Select(x => x.Comment));
            }

            public record CommentDto : BaseDto<StatusComment, CommentDto>
            {
                public long Id { get; set; }
                public DocumentsEnum DocumentType { get; set; }
                public string Text { get; set; }

                public override void AddCustomMappings()
                {
                    SetCustomMappings()
                        .Map(x => x.Id, y => y.CommentId)
                        .Map(x => x.DocumentType, y => y.Comment.DocumentType)
                        .Map(x => x.Text, y => y.Comment.Text);
                }
            }
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

        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(x => x.Document, y => y.Documents.Where(x => x.IsArchived == false).FirstOrDefault());
        }

    }
}

