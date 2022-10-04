using Domain.Common.Constants;
using Domain.Common.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Marathons.Commands.AddLogo
{
    public class AddLogoCommand : IRequest<HttpStatusCode>
    {
        public int marathonId { get; set; }
        public IFormFile logo { get; set; }
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
                .FirstAsync(x => x.Id == cmd.marathonId, include: source => source.Include(a => a.Logo));
            var oldLogo = marathon.Logo;
            var logo = await _savedFileService.UploadFile(cmd.logo, FileTypeEnum.Marathons);
            marathon.Logo = logo;
            await _unit.MarathonTranslationRepository.SaveAsync();
            await _savedFileService.DeleteFile(oldLogo);
            return HttpStatusCode.OK;

        }
    }
}
