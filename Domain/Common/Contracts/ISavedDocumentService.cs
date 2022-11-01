using System;
using Domain.Entities.Documents;
using Domain.Entities.Documents.DocumentEnums;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Http;

namespace Domain.Common.Contracts
{
    public interface ISavedDocumentService
    {
        Task UploadDocumentAsync(Status status, Document document, IFormFile file, DocumentsEnum documentType);
        void DeleteDocument(Status status, Document document, DocumentsEnum documentType);
    }
}

