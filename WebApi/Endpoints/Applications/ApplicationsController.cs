

using Core.UseCases.Applications.Commands.CraeteApplication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Applications.Dtos.Requests;

namespace WebApi.Endpoints.Applications;

[ApiController]
[Route("api/v{version:apiVersion}/applications")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class ApplicationsController : BaseController
{
    private readonly IMediator _mediator;

    public ApplicationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    ///// <summary>
    ///// List of marathons
    ///// </summary>
    //[HttpGet("", Name = "GetMarathons")]
    //[ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    //[ProducesResponseType(typeof(IEnumerable<GetMarathonsOutDto>), StatusCodes.Status200OK)]
    //public async Task<ActionResult<IQueryable<GetMarathonsOutDto>>> List(
    //    [FromQuery] GridifyQuery query)
    //{
    //    var getMarathonsQuery = new GetMarathonsQuery()
    //    {
    //        LanguageCode = this.Request.Headers["Accept-Language"],
    //        Query = query
    //    };

    //    var result = await _mediator.Send(getMarathonsQuery);

    //    return Ok(result);
    //}

    ///// <summary>
    ///// Get Marathon by id
    ///// </summary>
    ///// <response code="200">Marathon</response>
    //[HttpGet("{marathonId:int}", Name = "GetMarathon")]
    //[ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    //[ProducesResponseType(typeof(GetMarathonOutDto), StatusCodes.Status200OK)]
    //public async Task<ActionResult<GetMarathonOutDto>> ById(
    //    [FromRoute] int marathonId)
    //{
    //    var getMarathonQuery = new GetMarathonQuery()
    //    {
    //        LanguageCode = this.Request.Headers["Accept-Language"],
    //        MarathonId = marathonId,
    //    };

    //    var result = await _mediator.Send(getMarathonQuery);

    //    return Ok(result);
    //}


    /// <summary>
    /// Create Application
    /// </summary>
    /// <response code="200">Id of created application</response>
    [HttpPost("", Name = "CreateApplication")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<HttpStatusCode>> Create(
        [FromBody] CreateApplicationRequestDto dto
        )
    {
        var tmp = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var createApplicationCommand = new CreateApplicationCommand()
        {
            DistanceId = dto.DistanceId,
            Promocode = dto.Promocode,
            UserId = Convert.ToInt32(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value)
        };

        var result = await _mediator.Send(createApplicationCommand);

        return Ok();
    }

    ///// <summary>
    ///// Update marathon
    ///// </summary>x
    ///// <response code="200">Response stauts code</response>
    //[HttpPut("", Name = "UpdateMarathon")]
    //[ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    //[ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    //public async Task<ActionResult<HttpStatusCode>> Update(
    //    [FromBody] PutMarathonRequestDto dto,
    //    [FromServices] IValidator<PutMarathonRequestDto> validator)
    //{
    //    var validation = await validator.ValidateAsync(dto);

    //    if (!validation.IsValid)
    //    {
    //        return validation.ToBadRequest();
    //    }
    //    var createMarathonCommand = new PutMarathonCommand()
    //    {
    //        MarathonDto = dto.Adapt<PutMarathonInDto>(),
    //    };

    //    var result = await _mediator.Send(createMarathonCommand);

    //    return Ok(result);
    //}

    ///// <summary>
    ///// Add logo to Marathon
    ///// </summary>
    ///// <response code="200"></response>
    //[HttpPost("{marathonId:int}/logo")]
    //[Consumes("multipart/form-data")]
    //[ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    //[ProducesResponseType(typeof(AddLogoToMarathonRequestDto), StatusCodes.Status200OK)]
    //public async Task<ActionResult<AddLogoToMarathonRequestDto>> AddLogo(
    //    [FromRoute] int marathonId,
    //    [FromForm] AddLogoToMarathonRequestDto dto,
    //    [FromServices] IValidator<AddLogoToMarathonRequestDto> validator)
    //{
    //    var validation = await validator.ValidateAsync(dto);

    //    if (!validation.IsValid)
    //    {
    //        return validation.ToBadRequest();
    //    }

    //    var addLogoCommand = new AddLogoCommand()
    //    {
    //        MarathonId = marathonId,
    //        Logo = dto.Logo
    //    };

    //    var result = await _mediator.Send(addLogoCommand);

    //    return Ok(result);

    //}

    ///// <summary>
    ///// Delete logo from marathon
    ///// </summary>
    ///// <response code="200"></response>
    //[HttpDelete("{marathonId:int}/logo")]
    //[ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    //[ProducesResponseType(typeof(AddLogoToMarathonRequestDto), StatusCodes.Status200OK)]
    //public async Task<ActionResult<AddLogoToMarathonRequestDto>> DeleteLogo(
    //    [FromRoute] int marathonId)
    //{

    //    var deleteLogoCommand = new DeleteLogoCommand()
    //    {
    //        MarathonId = marathonId,
    //    };

    //    var result = await _mediator.Send(deleteLogoCommand);

    //    return Ok(result);

    //}
}
