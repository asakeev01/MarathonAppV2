using Domain.Common.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.UseCases.Marathons.Commands.DeleteLogo;

public class DeleteLogoCommand : IRequest<HttpStatusCode>
{
    public int MarathonId { get; set; }
    public int TranslationId { get; set; }
}

public class DeleteLogoCommandHandler : IRequestHandler<DeleteLogoCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedFileService _savedFileService;

    public DeleteLogoCommandHandler(IUnitOfWork unit, ISavedFileService savedFileService)
    {
        _unit = unit;
        _savedFileService = savedFileService;
    }

    public async Task<HttpStatusCode> Handle(DeleteLogoCommand cmd, CancellationToken cancellationToken)
    {

        var translation = await _unit.MarathonTranslationRepository
            .FirstAsync(x => x.Id == cmd.TranslationId && x.MarathonId == cmd.MarathonId, include: source => source.Include(a => a.Logo));
        var oldLogo = translation.Logo;
        translation.LogoId = null;
        translation.Logo = null;
        await _unit.MarathonTranslationRepository.SaveAsync();
        await _savedFileService.DeleteFile(oldLogo);
        return HttpStatusCode.OK;

    }
}
