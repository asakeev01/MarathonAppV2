using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Statuses;
using Mapster;
using MediatR;

namespace Core.UseCases.Statuses.Commands.CreateStatusCommentAsAdmin;

public class CreateStatusCommentCommand : IRequest<HttpStatusCode>
{
    public CreateStatusCommentInDto CommentDto { get; set; }
}

public class CreateStatusCommentHandler : IRequestHandler<CreateStatusCommentCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public CreateStatusCommentHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(CreateStatusCommentCommand cmd, CancellationToken cancellationToken)
    {
        var comment = cmd.CommentDto.Adapt<Comment>();
        await _unit.CommentRepository.CreateAsync(comment, save: true);
        return HttpStatusCode.OK;
    }
}


