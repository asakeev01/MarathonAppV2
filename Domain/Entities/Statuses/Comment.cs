using System;
using Domain.Entities.Documents.DocumentEnums;

namespace Domain.Entities.Statuses;

public class Comment
{
    public long Id { get; set; }
    public DocumentsEnum DocumentType { get; set; }
    public string Text { get; set; }
    public ICollection<StatusComment>? StatusComments { get; set; }
}

