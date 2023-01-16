using System;
using System.Net;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions.ErrorHandlingServices;

namespace WebApi.Endpoints.Payments;

[ApiController]
[Route("api/v{version:apiVersion}/payments")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class ApplicationsController : BaseController
{
    private readonly IMediator _mediator;

    public ApplicationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create Application with promocode
    /// </summary>
    /// <response code="200">Id of created application</response>
    [HttpPost("")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> ReceivePayment(
        [FromBody] CreateApplicationViaPromocodeRequestDto dto
        )
    {
        var createApplicationCommand = new CreateApplicationViaPromocodeCommand()
        {
            DistanceId = dto.DistanceId,
            Promocode = dto.Promocode,
            UserId = Convert.ToInt32(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value)
        };

        var result = await _mediator.Send(createApplicationCommand);

        return Ok(result);
    }
}

