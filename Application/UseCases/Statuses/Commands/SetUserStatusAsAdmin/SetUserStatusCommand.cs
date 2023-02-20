using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Statuses;
using Domain.Entities.Statuses.StatusEnums;
using Domain.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Statuses.Commands.SetUserStatusAsAdmin;
public class SetUserStatusCommand : IRequest<HttpStatusCode>
{
    public long UserId { get; set; }
    public StatusesEnum NewStatus { get; set; }
    public ICollection<long> CommentsId { get; set; }
}

public class SetUserStatusHandler : IRequestHandler<SetUserStatusCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IStatusService _statusService;

    public SetUserStatusHandler(IUnitOfWork unit, IStatusService statusService)
    {
        _unit = unit;
        _statusService = statusService;
    }

    public async Task<HttpStatusCode> Handle(SetUserStatusCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.FirstAsync(predicate: x => x.Id == cmd.UserId, include: source => source.Include(x => x.Documents));
        var document = await _unit.DocumentRepository.FirstAsync(d => d.UserId == cmd.UserId & d.IsArchived == false);
        var status = await _unit.StatusRepository.FirstAsync(s => s.UserId == cmd.UserId, include: source => source
            .Include(s => s.StatusComments));
        List<Comment> comments = null;
        if(cmd.CommentsId != null)
            comments = await _unit.CommentRepository.FindByCondition(c => cmd.CommentsId.Contains(c.Id)).ToListAsync();
        var statusCommentEntities = _statusService.SetUserStatus(identityUser, document, status, cmd.NewStatus, comments);
        foreach (var statusComment in status.StatusComments)
        {
            await _unit.StatusCommentRepository.Delete(statusComment);
        }
        foreach (var statusComment in statusCommentEntities)
        {
            await _unit.StatusCommentRepository.CreateAsync(statusComment, save: true);
        }
        await _unit.UserRepository.UpdateAsync(identityUser);
        await _unit.StatusRepository.SaveAsync();
        return HttpStatusCode.OK;
    }
}


