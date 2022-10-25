using System;
using Domain.Entities.Documents;
using Domain.Entities.Documents.DocumentEnums;
using Microsoft.AspNetCore.Http;

namespace Domain.Common.Contracts
{
    public interface ISavedDocumentService
    {
        Task<Document> UploadDocumentAsync(Document document, IFormFile file, DocumentsEnum documentType);
    }
}

