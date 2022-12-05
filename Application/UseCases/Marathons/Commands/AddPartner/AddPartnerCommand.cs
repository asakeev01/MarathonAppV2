using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Infrastructure.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Marathons.Commands.AddPartner;

public class AddPartnerCommand : IRequest<int>
{
    public int MarathonId { get; set; }
    public ICollection<IFormFile> Logos { get; set; }
    public AddPartnerCommandInDto PartnerDto { get; set; }
}

public class AddPartnerCommandHandler : IRequestHandler<AddPartnerCommand, int>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedFileService _savedFileService;

    public AddPartnerCommandHandler(IUnitOfWork unit, ISavedFileService savedFileService)
    {
        _unit = unit;
        _savedFileService = savedFileService;
    }

    public async Task<int> Handle(AddPartnerCommand cmd, CancellationToken cancellationToken)
    {
        //var marathon = await _unit.MarathonRepository
        //    .FirstAsync(x => x.Id == cmd.MarathonId, include: source => source.Include(a => a.Partners));

        //var entity = cmd.PartnerDto.Adapt<Partner>();
        //entity.Marathon = marathon;
        //var partner = await _unit.PartnerRepository.CreateAsync(entity, save: true);

        //foreach (var logo in cmd.Logos)
        //{
        //    var savedFile = await _savedFileService.UploadFile(logo, FileTypeEnum.Partners);
        //    savedFile.Partner = partner;
        //    await _unit.SavedFileRepository.SaveAsync();
        //}

        //await _unit.PartnerRepository.SaveAsync();
        //return partner.Id;
        return 0;
    }
}
