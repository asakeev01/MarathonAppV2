using System;
using Core.Common.Bases;
using Domain.Entities.Documents.DocumentEnums;
using Domain.Entities.Statuses;
using Domain.Entities.Users;

namespace Core.UseCases.Statuses.Queries.GetStatusCommentsAsAdmin;

public record GetStatusCommentsOutDto : BaseDto<Comment, GetStatusCommentsQuery>
{
    public long Id { get; set; }
    public DocumentsEnum DocumentType { get; set; }
    public string Text { get; set; }
}

