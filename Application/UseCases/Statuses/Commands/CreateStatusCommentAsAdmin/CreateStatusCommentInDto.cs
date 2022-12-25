using System;
using Core.Common.Bases;
using Domain.Entities.Documents.DocumentEnums;
using Domain.Entities.Statuses;

namespace Core.UseCases.Statuses.Commands.CreateStatusCommentAsAdmin;

public record CreateStatusCommentInDto : BaseDto<CreateStatusCommentInDto, Comment>
{
    public DocumentsEnum DocumentType { get; set; }
    public string Text { get; set; }
}

