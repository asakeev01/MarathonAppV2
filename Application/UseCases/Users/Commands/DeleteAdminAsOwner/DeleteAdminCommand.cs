using System;
using System.Net;
using Domain.Common.Contracts;
using MediatR;

namespace Core.UseCases.Users.Commands.DeleteAdminAsOwner;

public class DeleteAdminCommand : IRequest<HttpStatusCode>
{
    public long? UserId { get; set; }
}

public class DeleteAdminHandler : IRequestHandler<DeleteAdminCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public DeleteAdminHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(DeleteAdminCommand cmd, CancellationToken cancellationToken)
    {
        var user = await _unit.UserRepository.FirstAsync(predicate: u => u.Id == cmd.UserId);
        await _unit.UserRepository.DeleteAsync(user);
        //await _unit.UserRepository.Delete(user, save: true);
        return HttpStatusCode.OK;
    }
}

