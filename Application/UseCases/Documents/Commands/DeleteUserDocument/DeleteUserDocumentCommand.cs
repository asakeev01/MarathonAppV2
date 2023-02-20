using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Documents.DocumentEnums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Documents.Commands.DeleteUserDocument;

public class DeleteUserDocumentCommand : IRequest<HttpStatusCode>
{
    public string? UserId { get; set; }
    public DocumentsEnum DocumentType { get; set; }
}

public class DeleteUserDocumentHandler : IRequestHandler<DeleteUserDocumentCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedDocumentService _savedDocumentService;

    public DeleteUserDocumentHandler(IUnitOfWork unit, ISavedDocumentService savedDocumentService)
    {
        _unit = unit;
        _savedDocumentService = savedDocumentService;
    }

    public async Task<HttpStatusCode> Handle(DeleteUserDocumentCommand cmd, CancellationToken cancellationToken)
    {
        var status = await _unit.StatusRepository.FirstAsync(s => s.UserId == long.Parse(cmd.UserId), include: source => source
            .Include(s => s.User));
        var document = await _unit.DocumentRepository.FirstAsync(d => d.UserId == long.Parse(cmd.UserId) && d.IsArchived == false);
        _savedDocumentService.DeleteDocument(status, document, cmd.DocumentType);
        await _unit.DocumentRepository.SaveAsync();
        await _unit.StatusRepository.SaveAsync();
        await _unit.UserRepository.SaveAsync();
        return HttpStatusCode.OK;
    }
}

