using System;
namespace Domain.Entities.Statuses;

public class StatusComment
{
    public long Id { get; set; }
    public long CommentId { get; set; }
    public long StatusId { get; set; }

    public virtual Comment Comment { get; set; }
    public virtual Status Status { get; set; }
}

