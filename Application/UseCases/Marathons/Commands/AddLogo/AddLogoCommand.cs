using Domain.Common.Constants;
using Domain.Common.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.UseCases.Marathons.Commands.AddLogo;

public class AddLogoCommand : IRequest<HttpStatusCode>
{
    public int MarathonId { get; set; }
    public IFormFile Logo { get; set; }
}

public class AddLogoCommandHandler : IRequestHandler<AddLogoCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedFileService _savedFileService;

    public AddLogoCommandHandler(IUnitOfWork unit, ISavedFileService savedFileService)
    {
        _unit = unit;
        _savedFileService = savedFileService;
    }

    public async Task<HttpStatusCode> Handle(AddLogoCommand cmd, CancellationToken cancellationToken)
    {
        var marathon = await _unit.MarathonRepository
            .FirstAsync(x => x.Id == cmd.MarathonId, include: source => source.Include(a => a.Logo));

        var oldLogo = marathon.Logo;
        var logo = await _savedFileService.UploadFile(cmd.Logo, FileTypeEnum.Marathons);
        marathon.Logo = logo;
        await _unit.MarathonTranslationRepository.SaveAsync();

        if (oldLogo != null)
        {
            await _savedFileService.DeleteFile(oldLogo);
        }

        return HttpStatusCode.OK;

    }
}
