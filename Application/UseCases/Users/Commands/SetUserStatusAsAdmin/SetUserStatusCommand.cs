using System;
using System.Net;
using Domain.Common.Contracts;
using Domain.Entities.Users.UserEnums;
using Domain.Services.Interfaces;
using MediatR;

namespace Core.UseCases.Users.Commands.SetUserStatusAsAdmin;
public class SetUserStatusCommand : IRequest<HttpStatusCode>
{
    public long UserId { get; set; }
    public StatusesEnum NewStatus { get; set; }
    public CommentsEnum Comment { get; set; }
}

public class SetUserStatusHandler : IRequestHandler<SetUserStatusCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly IUserService _userService;

    public SetUserStatusHandler(IUnitOfWork unit, IUserService userService)
    {
        _unit = unit;
        _userService = userService;
    }

    public async Task<HttpStatusCode> Handle(SetUserStatusCommand cmd, CancellationToken cancellationToken)
    {
        var identityUser = await _unit.UserRepository.GetByIdAsync(cmd.UserId.ToString());
        var document = await _unit.DocumentRepository.FirstAsync(d => d.UserId == cmd.UserId);
        var status = await _unit.StatusRepository.FirstAsync(s => s.UserId == cmd.UserId);
        _userService.SetUserStatus(identityUser, document, status, cmd.NewStatus, cmd.Comment);
        await _unit.UserRepository.UpdateAsync(identityUser);
        await _unit.StatusRepository.SaveAsync();
        return HttpStatusCode.OK;
    }
}


