using Domain.Common.Constants;
using Domain.Common.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.UseCases.Marathons.Commands.AddPartnerLogo;

public class AddPartnerLogoCommand : IRequest<HttpStatusCode>
{
    public int PartnerId { get; set; }
    public ICollection<IFormFile> Logos { get; set; }
}

public class AddPartnerLogoHandler : IRequestHandler<AddPartnerLogoCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedFileService _savedFileService;

    public AddPartnerLogoHandler(IUnitOfWork unit, ISavedFileService savedFileService)
    {
        _unit = unit;
        _savedFileService = savedFileService;
    }

    public async Task<HttpStatusCode> Handle(AddPartnerLogoCommand cmd, CancellationToken cancellationToken)
    {
        //var partner = await _unit.PartnerRepository
        //    .FirstAsync(x => x.Id == cmd.PartnerId, include: source => source.Include(a => a.Logos));
        //foreach (var logo in cmd.Logos)
        //{
        //    var savedFile = await _savedFileService.UploadFile(logo, FileTypeEnum.Partners);
        //    savedFile.Partner = partner;
        //    await _unit.SavedFileRepository.SaveAsync();
        //}
        //await _unit.PartnerRepository.SaveAsync();
        return HttpStatusCode.OK;

    }
}
