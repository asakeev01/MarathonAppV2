using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Documents.DocumentEnums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
            var status = await _unit.StatusRepository.FirstAsync(s => s.UserId == long.Parse(cmd.UserId), include: source => source
                .Include(s => s.StatusComments)
                .Include(s => s.User));
            var document = await _unit.DocumentRepository.FirstAsync(d => d.UserId == long.Parse(cmd.UserId) && d.IsArchived == false);
            await _savedDocumentService.UploadDocumentAsync(status, document, cmd.Document, cmd.DocumentType);
            await _unit.DocumentRepository.SaveAsync();
            await _unit.StatusRepository.SaveAsync();
            await _unit.UserRepository.SaveAsync();
            return HttpStatusCode.OK;
        }
    }
}

