using System;
using Domain.Entities.Documents.DocumentEnums;
using FluentValidation;

namespace WebApi.Endpoints.Documents.Dtos.Requests
{
    public class DeleteUserDocumentRequestDto
    {
        public DocumentsEnum DocumentType { get; set; }
    }

    public class DeleteUserDocumentRequestValidator : AbstractValidator<DeleteUserDocumentRequestDto>
    {
    }
}

