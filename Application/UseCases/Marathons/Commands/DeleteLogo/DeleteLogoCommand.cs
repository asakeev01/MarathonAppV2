using Domain.Common.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Commands.DeleteLogo
{
    public class DeleteLogoCommand : IRequest<HttpStatusCode>
    {
        public int marathonId { get; set; }
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
            var marathon = await _unit.MarathonRepository
                .FirstAsync(x => x.Id == cmd.marathonId, include: source => source.Include(a => a.Logo));
            var oldLogo = marathon.Logo;
            marathon.LogoId = null;
            marathon.Logo = null;
            await _unit.MarathonTranslationRepository.SaveAsync();
            await _savedFileService.DeleteFile(oldLogo);
            return HttpStatusCode.OK;

        }
    }
}
