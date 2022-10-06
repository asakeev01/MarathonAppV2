using System.Net.Mime;
using Core.UseCases.Files.Commands.DeleteFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Marathons.Dtos.Requests;

namespace WebApi.Endpoints.Files;

[ApiController]
[Route("api/v{version:apiVersion}/files")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class FilesController : BaseController
{
    private readonly IMediator _mediator;

    public FilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Delete file
    /// </summary>
    /// <response code="200"></response>
    [HttpDelete("{fileId:int}")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(AddLogoToMarathonRequestDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<AddLogoToMarathonRequestDto>> DeleteFile(
        [FromRoute] int fileId)
    {
        var deleteFileCommand = new DeleteFileCommand()
        {
            FileId = fileId,
        };

        var result = await _mediator.Send(deleteFileCommand);

        return Ok(result);

    }
}
