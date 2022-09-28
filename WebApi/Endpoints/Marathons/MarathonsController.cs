using System.Net;
using System.Net.Mime;
using Core.UseCases.Marathons.Commands.CraeteMarathon;
using Core.UseCases.Marathons.Commands.CreateMarathon;
using Core.UseCases.Marathons.Queries.GetMarathon;
using Core.UseCases.Marathons.Queries.GetMarathons;
using FluentValidation;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Marathons.Dtos.Requests;

namespace WebApi.Endpoints.Accounts;

[Route("api/marathons")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class MarathonsController : BaseController
{
    private readonly IMediator _mediator;

    public MarathonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// List of marathons
    /// </summary>
    [HttpGet("")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(IEnumerable<GetMarathonsOutDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IQueryable<GetMarathonsOutDto>>> List(
        [FromQuery] GridifyQuery query)
    {
        var getMarathonsQuery = new GetMarathonsQuery()
        {
            LanguageCode = this.Request.Headers["Accept-Language"],
            Query = query
        };

        var result = await _mediator.Send(getMarathonsQuery);

        return Ok(result);
    }

    /// <summary>
    /// Get Marathon by id
    /// </summary>
    /// <response code="200">Marathon</response>
    [HttpGet("{marathonId:int}")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetMarathonOutDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetMarathonOutDto>> ById(
        [FromRoute] int marathonId)
    {
        var getMarathonQuery = new GetMarathonQuery()
        {
            LanguageCode = this.Request.Headers["Accept-Language"],
            MarathonId = marathonId,
        };

        var result = await _mediator.Send(getMarathonQuery);

        return Ok(result);
    }

    /// <summary>
    /// Create marathon with distances
    /// </summary>
    /// <response code="200">Id of created marathon</response>
    [HttpPost("")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> Create(
        [FromBody] CreateMarathonRequestDto dto,
        [FromServices] IValidator<CreateMarathonRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var createMarathonCommand = new CreateMarathonCommand()
        {
            marathonDto = dto.Adapt<CreateMarathonRequestInDto>(),
        };

        var result = await _mediator.Send(createMarathonCommand);

        return Ok(result);
    }

    ///// <summary>
    ///// Transfer balance from one account to another
    ///// </summary>
    ///// <response code="200"></response>
    //[HttpPatch("{accountId:int}/transfer")]
    //[ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[SwaggerRequestExample(typeof(TransferRequestDto), typeof(TransferRequestExamples))]
    //public async Task<ActionResult<HttpStatusCode>> Transfer(
    //    [FromBody] TransferRequestDto dto,
    //    [FromRoute] int accountId,
    //    [FromServices] IValidator<TransferRequestDto> validator)
    //{
    //    dto.AccountSenderId = accountId;

    //    var validation = await validator.ValidateAsync(dto);
    //    if (!validation.IsValid)
    //    {
    //        return validation.ToBadRequest();
    //    }

    //    var command = dto.Adapt<TransferCommand>();

    //    var result = await _mediator.Send(command);

    //    return result;
    //}

    ///// <summary>
    ///// Get Account by id
    ///// </summary>
    ///// <returns>New Updated Account</returns>
    ///// <response code="200">New Updated Account</response>
    //[HttpGet("{accountId:int}")]
    //[ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    //[ProducesResponseType(typeof(GetUserAccountOutDto), StatusCodes.Status200OK)]
    //[SwaggerResponseExample(200, typeof(GetAccountResponseExamples))]
    //public async Task<ActionResult<GetUserAccountOutDto>> ById(
    //    [FromRoute] int accountId,
    //    [FromQuery] GridifyQuery query)
    //{
    //    var getUserAccountQuery = new GetUserAccountQuery()
    //    {
    //        AccountId = accountId,
    //        UserId = UserService.GetCurrentUser(),
    //        Query = query
    //    };

    //    var result = await _mediator.Send(getUserAccountQuery);

    //    return Ok(result);
    //}
}