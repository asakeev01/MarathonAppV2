using System.Net;
using System.Net.Mime;
using Core.UseCases.Marathons.Commands.CraeteMarathon;
using Core.UseCases.Marathons.Commands.CreateMarathon;
using Core.UseCases.Marathons.Commands.PutMarathon;
using Core.UseCases.Marathons.Commands.PutMarathonStatus;
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

namespace WebApi.Endpoints.Accounts;

[ApiController]
[Route("api/v{version:apiVersion}/marathons")]
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
    [HttpGet("", Name = "GetMarathons")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(QueryablePaging<GetMarathonsOutDto>), StatusCodes.Status200OK)]
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
    [HttpGet("{marathonId:int}", Name = "GetMarathon")]
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
    /// Get Marathon by id for Admin
    /// </summary>
    /// <response code="200">Marathon</response>
    [HttpGet("admin/{marathonId:int}", Name = "GetMarathonAsAdmin")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetMarathonAdminOutDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetMarathonAdminOutDto>> ByIdAdmin(
        [FromRoute] int marathonId)
    {
        var getMarathonQuery = new GetMarathonAdminQuery()
        {
            MarathonId = marathonId,
        };

        var result = await _mediator.Send(getMarathonQuery);

        return Ok(result);
    }

    /// <summary>
    /// Create marathon with distances
    /// </summary>
    /// <response code="200">Id of created marathon</response>
    [HttpPost("", Name = "CreateMarathon")]
    [Consumes("multipart/form-data")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    public async Task<ActionResult<HttpStatusCode>> Create(
        [FromForm] CreateMarathonRequestDto dto,
        [FromServices] IValidator<CreateMarathonRequestDto> validator
        )
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var createMarathonCommand = new CreateMarathonCommand()
        {
            MarathonDto = dto.Adapt<CreateMarathonInDto>(),
            Documents = dto.Documents,
            PartnerCompanyLogos = dto.Partners.SelectMany(x => x.PartnerCompanies.Select(y => new PartnerCompanyLogo
            {
                SerialNumber = x.SerialNumber,
                Name = y.Name,
                Logo = y.Logo
            })
            ).ToList(),
            MarathonLogo = dto.Translations.Select(x => new MarathonLogos
            {
                LanguageId = x.LanguageId,
                Logo = x.Logo
            }
            ).ToList(),

        };

        var result = await _mediator.Send(createMarathonCommand);

        return Ok(result);
        return Ok();
    }

    /// <summary>
    /// Update marathon
    /// </summary>
    /// <response code="200">Response stauts code</response>
    [HttpPut("", Name = "ChangeMarathon")]
    [Consumes("multipart/form-data")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> Update(
        [FromForm] PutMarathonRequestDto dto,
        [FromServices] IValidator<PutMarathonRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var createMarathonCommand = new PutMarathonCommand()
        {
            MarathonDto = dto.Adapt<PutMarathonInDto>(),
            Documents = dto.Documents,
            PartnerCompanyLogos = dto.Partners.SelectMany(x => x.PartnerCompanies.Select(y => new UpdatePartnerCompanyLogo
            {
                SerialNumber = x.SerialNumber,
                Name = y.Name,
                Logo = y.Logo
            })
            ).ToList(),
            MarathonLogo = dto.Translations.Select(x => new UpdateMarathonLogos
            {
                LanguageId = x.LanguageId,
                Logo = x.Logo
            }
            ).ToList(),

        };

        var result = await _mediator.Send(createMarathonCommand);

        return Ok(result);
    }

    /// <summary>
    /// Update marathon`s status
    /// </summary>
    /// <response code="200">Response stauts code</response>
    [HttpPut("status", Name = "ChangeMarathonStatus")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> UpdateMarathonStatus(
        [FromBody] UpdateMarathonStatusRequestDto dto,
        [FromServices] IValidator<UpdateMarathonStatusRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var putMarathonStatusCommand = new PutMarathonStatusCommand()
        {
            MarathonId = dto.MarathonId,
            IsActive = dto.IsActive

        };

        var result = await _mediator.Send(putMarathonStatusCommand);

        return Ok(result);
    }
}
