using System;
using System.Net;
using Domain.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Statuses.Commands.DeleteStatusCommentAsAdmin;

public class DeleteStatusCommentCommand : IRequest<HttpStatusCode>
{
    public string CommentId { get; set; }
}

public class DeleteUserDocumentHandler : IRequestHandler<DeleteStatusCommentCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public DeleteUserDocumentHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(DeleteStatusCommentCommand cmd, CancellationToken cancellationToken)
    {
        var comment = await _unit.CommentRepository.FirstAsync(c => c.Id == long.Parse(cmd.CommentId), include: source => source
            .Include(s => s.StatusComments));
        await _unit.CommentRepository.Delete(comment, save: true);
        return HttpStatusCode.OK;
    }
}


