using Domain.Common.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.UseCases.Marathons.Commands.DeletePartner;

public class DeletePartnerCommand : IRequest<HttpStatusCode>
{
    public int PartnerId { get; set; }
}

public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedFileService _savedFileService;

    public DeletePartnerCommandHandler(IUnitOfWork unit, ISavedFileService savedFileService)
    {
        _unit = unit;
        _savedFileService = savedFileService;
    }

    public async Task<HttpStatusCode> Handle(DeletePartnerCommand cmd, CancellationToken cancellationToken)
    {
        var partner = await _unit.PartnerRepository
            .FirstAsync(x => x.Id == cmd.PartnerId, include: source => source.Include(a => a.Logos).Include(a => a.Translations));
        foreach(var logo in partner.Logos)
        {
            await _savedFileService.DeleteFile(logo);
        }
        await _unit.PartnerRepository.Delete(partner, save:true);
        return HttpStatusCode.OK;

    }
}
