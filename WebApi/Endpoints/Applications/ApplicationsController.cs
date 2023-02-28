using Core.UseCases.Applications.Commands.CreateApplicationViaMoney;
using Core.UseCases.Applications.Commands.CreateApplicationForPWD;
using Core.UseCases.Applications.Commands.CreateApplicationViaPromocode;
using Core.UseCases.Applications.Commands.ImportExcelApplications;
using Core.UseCases.Applications.Commands.IssueStarterKit;
using Core.UseCases.Applications.Queries.ApplicationById;
using Core.UseCases.Applications.Queries.ApplicationByStarterKitCodeQuery;
using Core.UseCases.Applications.Queries.ApplicationsByMarathonQuery;
using Core.UseCases.Applications.Queries.GenerateExcelApplications;
using Core.UseCases.Applications.Queries.MyApplications;
using FluentValidation;
using Gridify;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Applications.Dtos.Requests;
using Domain.Entities.Users.Constants;

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

    /// <summary>
    /// Create Application with promocode
    /// </summary>
    /// <response code="200">Id of created application</response>
    [HttpPost("")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<HttpStatusCode>> CreateViaPromocode(
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

    /// <summary>
    /// Create Application For PWD
    /// </summary>
    /// <response code="200">Id of created application</response>
    [HttpPost("pwd")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<HttpStatusCode>> CreateForPWD(
        [FromBody] CreateApplicationForPWDRequestDto dto
        )
    {
        var createApplicationForPWDCommand = new CreateApplicationForPWDCommand()
        {
            DistanceId = dto.DistanceId,
            UserId = Convert.ToInt32(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value)
        };

        var result = await _mediator.Send(createApplicationForPWDCommand);

        return Ok(result);
    }

    /// <summary>
    /// Create Application with payment
    /// </summary>
    /// <response code="200">Url of payment</response>
    [HttpPost("money")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<HttpStatusCode>> CreatePayment(
        [FromBody] CreateApplicationViaMoneyRequestDto dto
        )
    {
        var createPaymentCommand = new CreateApplicationViaMoneyCommand()
        {
            DistanceId = dto.DistanceId,
            UserId = Convert.ToInt32(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value)
        };

        var result = await _mediator.Send(createPaymentCommand);

        return Ok(result);
    }

    /// <summary>
    /// Get Application by Id
    /// </summary>
    [HttpGet("{applicationId}")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(ApplicationByIdQueryOutDto), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin + "," + Roles.Volunteer)]
    public async Task<ActionResult<HttpStatusCode>> Create(
        [FromRoute] int applicationId
        )
    {
        var applicationByIdQuery = new ApplicationByIdQuery()
        {
            ApplicationId = applicationId
        };

        var result = await _mediator.Send(applicationByIdQuery);

        return Ok(result);
    }

    /// <summary>
    /// Get applications by marathon ID
    /// </summary>
    [HttpGet("marathon/{marathonId}")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(QueryablePaging<ApplicationByMarathonQueryOutDto>), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin + "," + Roles.Volunteer)]
    public async Task<ActionResult<HttpStatusCode>> ApplicationsByMarathon(
        [FromRoute] int marathonId,
        [FromQuery] GridifyQuery query)
    {
        var applicationByMarathonQuery = new ApplicationByMarathonQuery()
        {
            MarathonId = marathonId,
            Query = query
        };

        var result = await _mediator.Send(applicationByMarathonQuery);

        return Ok(result);
    }

    /// <summary>
    /// Export Applications to excel
    /// </summary>
    [HttpGet("marathon/{marathonId}/excel")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin)]
    public async Task<ActionResult> GenerateExcelPromocodes(
        [FromRoute] int marathonId
        )
    {
        var generateExcelApplicationsQuery = new GenerateExcelApplicationsQuery()
        {
            MarathonId = marathonId,
        };

        var (result, marathonName) = await _mediator.Send(generateExcelApplicationsQuery);
        HttpContext.Response.Headers.Add("content-disposition", $"attachment; filename=Applications_{System.Net.WebUtility.UrlEncode(marathonName)}_{DateTime.Now.ToString("dd/MM/yyyy")}.xlsx");
        HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
        this.Response.ContentType = "application/vnd.ms-excel";
        return File(result, "application/vnd.ms-excel");
    }

    /// <summary>
    /// Import Excel
    /// </summary>
    [HttpPut("marathon/{marathonId}/excel")]
    [Consumes("multipart/form-data")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin)]
    public async Task<ActionResult> ImportExcel(
        [FromRoute] int marathonId,
        [FromForm] ImportExcelApplicationsRequestDto dto,
        [FromServices] IValidator<ImportExcelApplicationsRequestDto> validator
        )
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var importExcelApplicationsCommand = new ImportExcelApplicationsCommand()
        {
            MarathonId = marathonId,
            ExcelFile = dto.ExcelFile
        };

        var result = await _mediator.Send(importExcelApplicationsCommand);
        return Ok(result);
    }

    [HttpGet("starterkit/{marathonId}/{starterKitCode}")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(ApplicationByStarterKitCodeQueryOutDto), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin + "," + Roles.Volunteer)]
    public async Task<ActionResult<HttpStatusCode>> ApplicationsByStarterKitCode(
    [FromRoute] string starterKitCode,
    [FromRoute] int marathonId)
    {
        var applicationByStarterKitCodeQuery = new ApplicationByStarterKitCodeQuery()
        {
            StarterKitCode = starterKitCode,
            MarathonId = marathonId
        };

        var result = await _mediator.Send(applicationByStarterKitCodeQuery);

        return Ok(result);
    }

    [HttpPut("starterkit/{applicationId}")]
    [Consumes("multipart/form-data")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin + "," + Roles.Volunteer)]
    public async Task<ActionResult> IssueStarterKit(
        [FromRoute] int applicationId,
        [FromForm] IssueStarterKitRequestDto dto,
        [FromServices] IValidator<IssueStarterKitRequestDto> validator
        )
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var issueStarterKitCommand = new IssueStarterKitCommand()
        {
            StarterKit = dto.StarterKit,
            ApplicationId = applicationId,
            FullNameRecipient = dto.FullNameRecipient
        };

        var result = await _mediator.Send(issueStarterKitCommand);
        return Ok(result);
    }

    [HttpGet("myApplications")]
    [Consumes("multipart/form-data")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(MyApplicationsQueryOutDto), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult> MyApplications(
        [FromQuery] GridifyQuery query
        )
    {

        var issueStarterKitCommand = new MyApplicationsQuery()
        {
            UserId = Convert.ToInt32(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value),
            LanguageCode = this.Request.Headers["Accept-Language"],
            Query = query,
        };

        var result = await _mediator.Send(issueStarterKitCommand);
        return Ok(result);
    }
}