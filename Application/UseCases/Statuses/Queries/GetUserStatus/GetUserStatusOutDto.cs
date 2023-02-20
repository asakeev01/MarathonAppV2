using System;
using Core.Common.Bases;
using Domain.Entities.Documents.DocumentEnums;
using Domain.Entities.Statuses;
using Domain.Entities.Statuses.StatusEnums;

namespace Core.UseCases.Statuses.Queries.GetUserStatus;

public record GetUserStatusOutDto : BaseDto<Status, GetUserStatusOutDto>
{
    public long Id { get; set; }
    public StatusesEnum CurrentStatus { get; set; }
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


