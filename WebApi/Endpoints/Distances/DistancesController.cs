using System.Net;
using System.Net.Mime;
using Core.UseCases.Distances.Commands.CreateDistanceCategory;
using Core.UseCases.Distances.Queries.GetDistanceCategories;
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
using WebApi.Endpoints.Distances.Dtos.Requests;
using WebApi.Endpoints.Marathons.Dtos.Requests;

namespace WebApi.Endpoints.Distances;

[Route("api/distances")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class DistancesController : BaseController
{
    private readonly IMediator _mediator;

    public DistancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// List of distance categories
    /// </summary>
    [HttpGet("categories")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(IEnumerable<GetDistanceCategoriesOutDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IQueryable<GetDistanceCategoriesOutDto>>> ListCategories(
        [FromQuery] GridifyQuery query)
    {
        var getDistanceCategoriesQuery = new GetDistanceCategoriesQuery()
        {
            LanguageCode = this.Request.Headers["Accept-Language"],
            Query = query
        };

        var result = await _mediator.Send(getDistanceCategoriesQuery);

        return Ok(result);
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

    /// <summary>
    /// Get DistanceCategory by id for Admin
    /// </summary>
    /// <response code="200">Marathon</response>
    [HttpGet("admin/{distanceCategoryId:int}")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetDistanceCategoriesOutDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetDistanceCategoriesOutDto>> CategoryByIdAdmin(
        [FromRoute] int distanceCategoryId)
    {
        var getDistanceCategoriesAdminQuery = new GetDistanceCategoriesAdminQuery()
        {
            DistanceCategoryId = distanceCategoryId,
        };

        var result = await _mediator.Send(getDistanceCategoriesAdminQuery);

        return Ok(result);
    }

    /// <summary>
    /// Create distance category
    /// </summary>
    /// <response code="200">Id of created category</response>
    [HttpPost("")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> CreateCategory(
        [FromBody] CreateDistanceCategoryRequestDto dto,
        [FromServices] IValidator<CreateDistanceCategoryRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var createDistanceCategoryCommand = new CreateDistanceCategoryCommand()
        {
            distanceCategoryDto = dto.Adapt<CreateDistanceCategoryInDto>(),
        };

        var result = await _mediator.Send(createDistanceCategoryCommand);

        return Ok(result);
    }

    ///// <summary>
    ///// Update marathon
    ///// </summary>
    ///// <response code="200">Response stauts code</response>
    //[HttpPut("")]
    //[ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    //[ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    //public async Task<ActionResult<HttpStatusCode>> Update(
    //    [FromBody] PutMarathonRequestDto dto)
    //{
    //    //    [FromServices] IValidator<PutMarathonRequestDto> validator)
    //    //{
    //    //    var validation = await validator.ValidateAsync(dto);

    //    //    if (!validation.IsValid)
    //    //    {
    //    //        return validation.ToBadRequest();
    //    //    }
    //    var createMarathonCommand = new PutMarathonCommand()
    //    {
    //        marathonDto = dto.Adapt<PutMarathonInDto>(),
    //    };

    //    var result = await _mediator.Send(createMarathonCommand);

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