using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Documents.DocumentEnums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.UseCases.Documents.Commands.UploadUserDocument
{
    public class UploadUserDocumentCommand : IRequest<HttpStatusCode>
    {
        public string? UserId { get; set; }
        public DocumentsEnum DocumentType { get; set; }
        public IFormFile Document { get; set; }
    }

    public class UploadUserDocumentHandler : IRequestHandler<UploadUserDocumentCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork _unit;
        private readonly ISavedDocumentService _savedDocumentService;

        public UploadUserDocumentHandler(IUnitOfWork unit, ISavedDocumentService savedDocumentService)
        {
            _unit = unit;
            _savedDocumentService = savedDocumentService;
        }

        public async Task<HttpStatusCode> Handle(UploadUserDocumentCommand cmd, CancellationToken cancellationToken)
        {
            var document = await _unit.DocumentRepository.FirstAsync(x => x.UserId == long.Parse(cmd.UserId));
            document = await _savedDocumentService.UploadDocumentAsync(document, cmd.Document, cmd.DocumentType);
            await _unit.DocumentRepository.SaveAsync();
            return HttpStatusCode.OK;
        }
    }
}

