using Domain.Common.Constants;
using Domain.Common.Contracts;
using Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.UseCases.Marathons.Commands.AddDocuments;

public class AddDocumentsCommand : IRequest<HttpStatusCode>
{
    public int MarathonId { get; set; }
    public ICollection<IFormFile> Documents { get; set; }
}

public class AddDocumentsCommandHandler : IRequestHandler<AddDocumentsCommand, HttpStatusCode>
{
    private readonly IUnitOfWork _unit;
    private readonly ISavedFileService _savedFileService;

    public AddDocumentsCommandHandler(IUnitOfWork unit, ISavedFileService savedFileService)
    {
        _unit = unit;
        _savedFileService = savedFileService;
    }

    public async Task<HttpStatusCode> Handle(AddDocumentsCommand cmd, CancellationToken cancellationToken)
    {
        var marathon = await _unit.MarathonRepository
            .FirstAsync(x => x.Id == cmd.MarathonId, include: source => source.Include(a => a.Documents));

        foreach(var document in cmd.Documents)
        {
            var savedFile = await _savedFileService.UploadFile(document, FileTypeEnum.Documents);
            savedFile.Marathon = marathon;
            await _unit.SavedFileRepository.SaveAsync();
        }
        return HttpStatusCode.OK;

    }
}
