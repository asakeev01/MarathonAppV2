using System;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using Core.UseCases.Applications.Commands.ImportExcelApplications;
using Core.UseCases.Results.Commands.SetResultsByExcel;
using Core.UseCases.Results.Queries.GetMyResults;
using Core.UseCases.Results.Queries.GetResultsByMarathon;
using Core.UseCases.Results.Queries.PrintResult;
using Domain.Entities.Marathons;
using Domain.Entities.Results;
using Domain.Entities.Users.Constants;
using FluentValidation;
using Gridify;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Applications.Dtos.Requests;
using WebApi.Endpoints.Results.Dtos;

namespace WebApi.Endpoints.Results;

[ApiController]
[Route("api/v{version:apiVersion}/results")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class ResultsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContext;

    public ResultsController(IMediator mediator, IHttpContextAccessor httpContext)
    {
        _mediator = mediator;
        _httpContext = httpContext;
    }

    [HttpGet("my", Name = "GetUserResults")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(QueryablePaging<GetMyResultOutDto>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<QueryablePaging<GetResultsByMarathonOutDto>>> GetMyResults([FromQuery] GridifyQuery query)
    {
        var id = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var getMyResultsQuery = new GetMyResultsQuery()
        {
            UserId = int.Parse(id),
            Query = query,
            LanguageCode = this.Request.Headers["Accept-Language"],
        };
        var result = await _mediator.Send(getMyResultsQuery);

        return Ok(result);
    }

    [HttpGet("marathon/{marathonId}", Name = "GetResultsByMarathonId")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(QueryablePaging<GetResultsByMarathonOutDto>), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<QueryablePaging<GetResultsByMarathonOutDto>>> GetResultsByMarathon([FromQuery] GridifyQuery query, [FromRoute] int marathonId)
    {
        var id = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var getResultsByMarathonQuery = new GetResultsByMarathonQuery()
        {
            MarathonId = marathonId,
            Query = query,
            LanguageCode = this.Request.Headers["Accept-Language"],
        };
        var result = await _mediator.Send(getResultsByMarathonQuery);

        return Ok(result);
    }


    [HttpPost("excel/{marathonId}", Name = "SetResultsByExcel")]
    [Consumes("multipart/form-data")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin)]
    public async Task<ActionResult<HttpStatusCode>> SetResultsByExcel(
        [FromRoute] int marathonId,
        [FromForm] SetResultsByExcelDto dto,
        [FromServices] IValidator<SetResultsByExcelDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var setResultByExcelCommand = new SetResultByExcelCommand()
        {
            MarathonId = marathonId,
            ExcelFile = dto.ExcelFile
        };

        var result = await _mediator.Send(setResultByExcelCommand);
        return Ok(result);
    }


    [HttpGet("{resultId}/print", Name = "GetPrintForResult")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(PrintResultOutDto), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<HttpStatusCode>> GetPrintByResult([FromQuery] GridifyQuery query, [FromRoute] int resultId)
    {
        var id = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var printResultQuery = new PrintResultQuery()
        {
            ResultId = resultId,
        };
        var result = await _mediator.Send(printResultQuery);

        //HttpContext.Response.Headers.Add("content-disposition", $"attachment; filename=My_Certificate.pdf");
        //HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
        //this.Response.ContentType = "application/pdf";
        //return File(result, "application/pdf");
        return Ok(result);
    }
}
