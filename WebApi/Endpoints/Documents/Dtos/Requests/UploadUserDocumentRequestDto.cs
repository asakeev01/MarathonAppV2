using System;
using Domain.Entities.Documents.DocumentEnums;
using FluentValidation;

namespace WebApi.Endpoints.Documents.Dtos.Requests;

public class UploadUserDocumentRequestDto
{
    public DocumentsEnum DocumentType { get; set; }
    public IFormFile Document { get; set; }
}

public class UploadUserDocumentRequestValidator : AbstractValidator<UploadUserDocumentRequestDto>
{
    public UploadUserDocumentRequestValidator()
    {
        RuleFor(x => x.Document.ContentType).Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
            .WithMessage("Only images are allowed");
        RuleFor(x => x.Document.Length).NotNull().LessThanOrEqualTo(20 * 1024 * 1024)
            .WithMessage("File size is larger than allowed");
    }
}


