using System.Net;
using System.Net.Mime;
using Core.UseCases.Marathons.Commands.CraeteMarathon;
using Core.UseCases.Marathons.Commands.CreateMarathon;
using Core.UseCases.Marathons.Commands.PutMarathon;
using Core.UseCases.Marathons.Queries.GetMarathon;
using Core.UseCases.Marathons.Queries.GetMarathonAdmin;
using Core.UseCases.Marathons.Queries.GetMarathons;
using FluentValidation;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Marathons.Dtos.Requests;

namespace WebApi.Endpoints.Distances;

[ApiController]
[Route("api/v{version:apiVersion}/distances")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class DistancesController : BaseController
{
    private readonly IMediator _mediator;

    public DistancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    ///// <summary>
    ///// Get Marathon by id
    ///// </summary>
    ///// <response code="200">Marathon</response>
    //[HttpGet("{marathonId:int}")]
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