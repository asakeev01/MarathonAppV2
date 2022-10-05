using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Entities.Marathons;
using Domain.Entities.SavedFiles;
using Infrastructure.Services.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Commands.AddPartners
{
    public class AddPartnerCommand : IRequest<int>
    {
        public int marathonId { get; set; }
        public ICollection<IFormFile> logos { get; set; }
        public AddPartnerCommandInDto partnerDto { get; set; }
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
            var marathon = await _unit.MarathonRepository
                .FirstAsync(x => x.Id == cmd.marathonId, include: source => source.Include(a => a.Partners));

            var entity = cmd.partnerDto.Adapt<Partner>();
            entity.Marathon = marathon;
            var partner = await _unit.PartnerRepository.CreateAsync(entity, save: true);
            foreach (var logo in cmd.logos)
            {
                var savedFile = await _savedFileService.UploadFile(logo, FileTypeEnum.Partners);
                savedFile.Partner = partner;
                await _unit.SavedFileRepository.SaveAsync();
            }
            await _unit.PartnerRepository.SaveAsync();
            return partner.Id;

        }
    }
}
