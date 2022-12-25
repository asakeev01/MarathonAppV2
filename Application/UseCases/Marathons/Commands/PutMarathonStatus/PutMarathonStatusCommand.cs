using Domain.Common.Contracts;
using MediatR;
using System.Net;

namespace Core.UseCases.Marathons.Commands.PutMarathonStatus;

public class PutMarathonStatusCommand : IRequest<HttpStatusCode>
{
    public int MarathonId { get; set; }
    public bool IsActive { get; set; }
}

public class PutMarathonStatusCommandHandler : IRequestHandler<PutMarathonStatusCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;

    public PutMarathonStatusCommandHandler(IUnitOfWork unit)
    {
        _unit = unit;
    }

    public async Task<HttpStatusCode> Handle(PutMarathonStatusCommand cmd, CancellationToken cancellationToken)
    {
        var marathon = await _unit.MarathonRepository.FirstAsync(x => x.Id == cmd.MarathonId);
        marathon.IsActive = cmd.IsActive;
        await _unit.MarathonRepository.Update(marathon, save: true);
        return HttpStatusCode.OK;
    }
}
