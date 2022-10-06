using System.Net;
using System.Net.Mime;
using Core.UseCases.Marathons.Commands.AddDocuments;
using Core.UseCases.Marathons.Commands.AddLogo;
using Core.UseCases.Marathons.Commands.AddPartnerLogo;
using Core.UseCases.Marathons.Commands.AddPartners;
using Core.UseCases.Marathons.Commands.CraeteMarathon;
using Core.UseCases.Marathons.Commands.CreateMarathon;
using Core.UseCases.Marathons.Commands.DeleteLogo;
using Core.UseCases.Marathons.Commands.DeletePartner;
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
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> Create(
        [FromBody] CreateMarathonRequestDto dto,
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
            marathonDto = dto.Adapt<CreateMarathonRequestInDto>()
        };

        var result = await _mediator.Send(createMarathonCommand);

        return Ok(result);
    }

    /// <summary>
    /// Update marathon
    /// </summary>
    /// <response code="200">Response stauts code</response>
    [HttpPut("", Name = "ChangeMarathon")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> Update(
        [FromBody] PutMarathonRequestDto dto,
        [FromServices] IValidator<PutMarathonRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var createMarathonCommand = new PutMarathonCommand()
        {
            marathonDto = dto.Adapt<PutMarathonInDto>(),
        };

        var result = await _mediator.Send(createMarathonCommand);

        return Ok(result);
    }

    /// <summary>
    /// Add logo to Marathon
    /// </summary>
    /// <response code="200">/response>
    [HttpPost("{marathonId:int}/logo")]
    [Consumes("multipart/form-data")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(AddLogoToMarathonRequestDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<AddLogoToMarathonRequestDto>> AddLogo(
        [FromRoute] int marathonId,
        [FromForm] AddLogoToMarathonRequestDto dto,
        [FromServices] IValidator<AddLogoToMarathonRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }

        var addLogoCommand = new AddLogoCommand()
            {
                marathonId = marathonId,
                logo = dto.Logo
            };

            var result = await _mediator.Send(addLogoCommand);

            return Ok(result);

    }

    /// <summary>
    /// Delete logo from marathon
    /// </summary>
    /// <response code="200"></response>
    [HttpDelete("{marathonId:int}/logo")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(AddLogoToMarathonRequestDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<AddLogoToMarathonRequestDto>> DeleteLogo(
        [FromRoute] int marathonId)
    {

            var deleteLogoCommand = new DeleteLogoCommand()
            {
                marathonId = marathonId,
            };

            var result = await _mediator.Send(deleteLogoCommand);

            return Ok(result);

    }

    /// <summary>
    /// Add logo to Marathon
    /// </summary>
    /// <response code="200">/response>
    [HttpPost("{marathonId:int}/partners")]
    [Consumes("multipart/form-data", "application/json")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(AddLogoToMarathonRequestDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<AddLogoToMarathonRequestDto>> AddPartners(
        [FromRoute] int marathonId,
        [FromForm] AddPartnersRequestDto dto,
        [FromServices] IValidator<AddPartnersRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }

        var addPartnerCommand = new AddPartnerCommand()
        {
            marathonId = marathonId,
            partnerDto = dto.Adapt<AddPartnerCommandInDto>(),
            logos = dto.Logos,
        };

        var result = await _mediator.Send(addPartnerCommand);

        return Ok(result);
    }

    /// <summary>
    /// Add documents to marathon
    /// </summary>
    /// <response code="200">/response>
    [HttpPost("{marathonId:int}/documents")]
    [Consumes("multipart/form-data", "application/json")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(AddDocumentsToMarathonRequestDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<AddDocumentsToMarathonRequestDto>> AddPartners(
        [FromRoute] int marathonId,
        [FromForm] AddDocumentsToMarathonRequestDto dto,
        [FromServices] IValidator<AddDocumentsToMarathonRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }

        var addDocumentsCommand = new AddDocumentsCommand()
        {
        
            MarathonId = marathonId,
            Documents = dto.Documents,
        };

        var result = await _mediator.Send(addDocumentsCommand);

        return Ok(result);
    }


    /// <summary>
    /// Delete partner
    /// </summary>
    /// <response code="200"></response>
    [HttpDelete("{partnerId:int}/partner")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(AddLogoToMarathonRequestDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<AddLogoToMarathonRequestDto>> DeletePartner(
        [FromRoute] int partnerId)
    {

        var deletePartnerCommand = new DeletePartnerCommand()
        {
            PartnerId = partnerId,
        };

        var result = await _mediator.Send(deletePartnerCommand);

        return Ok(result);

    }

    /// <summary>
    /// Add logos to partner
    /// </summary>
    /// <response code="200"></response>
    [HttpPost("partner/logo/{partnerId:int}")]
    [Consumes("multipart/form-data", "application/json")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(AddLogoToMarathonRequestDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<AddLogoToMarathonRequestDto>> AddLogoPartner(
        [FromRoute] int partnerId,
        [FromForm] ICollection<IFormFile> logos)
    {

        var addPartnerLogo = new AddPartnerLogo()
        {
            PartnerId = partnerId,
            Logos = logos,
        };

        var result = await _mediator.Send(addPartnerLogo);

        return Ok(result);

    }
}