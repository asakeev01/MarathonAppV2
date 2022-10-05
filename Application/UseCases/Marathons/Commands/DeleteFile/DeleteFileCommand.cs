using Core.UseCases.Marathons.Commands.DeletePartner;
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

namespace Core.UseCases.Marathons.Commands.DeletePartnerLogo
{
    public class DeleteFileCommand : IRequest<HttpStatusCode>
    {
        public int FileId { get; set; }
    }

    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork _unit;
        private readonly ISavedFileService _savedFileService;

        public DeleteFileCommandHandler(IUnitOfWork unit, ISavedFileService savedFileService)
        {
            _unit = unit;
            _savedFileService = savedFileService;
        }

        public async Task<HttpStatusCode> Handle(DeleteFileCommand cmd, CancellationToken cancellationToken)
        {
            var file = await _unit.SavedFileRepository
                .FirstAsync(x => x.Id == cmd.FileId);
            await _savedFileService.DeleteFile(file);
            await _unit.SavedFileRepository.SaveAsync();
            return HttpStatusCode.OK;
        }
    }
}
