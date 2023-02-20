using System;
using Domain.Entities.Documents.DocumentEnums;
using FluentValidation;

namespace WebApi.Endpoints.Users.Dtos.Requests;

public class CreateStatusCommentRequestDto
{
    public DocumentsEnum DocumentType { get; set; }
    public string Text { get; set; }
}

public class CreateStatusCommentRequestValidator : AbstractValidator<CreateStatusCommentRequestDto>
{
    public CreateStatusCommentRequestValidator()
    {
        RuleFor(x => x.Text)
        .NotEmpty();
    }
}


