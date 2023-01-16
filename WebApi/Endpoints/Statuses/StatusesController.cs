using System;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using Core.UseCases.Statuses.Commands.CreateStatusCommentAsAdmin;
using Core.UseCases.Statuses.Commands.DeleteStatusCommentAsAdmin;
using Core.UseCases.Statuses.Commands.SetUserStatusAsAdmin;
using Core.UseCases.Statuses.Queries.GetStatusCommentsAsAdmin;
using Core.UseCases.Statuses.Queries.GetUserStatus;
using Core.UseCases.Users.Commands.UpdateUserAsAdmin;
using Core.UseCases.Users.Queries.GetUserProfile;
using FluentValidation;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Users.Dtos.Requests;

namespace WebApi.Endpoints.Statuses;

[ApiController]
[Route("api/v{version:apiVersion}/statuses")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class StatusesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContext;

    public StatusesController(IMediator mediator, IHttpContextAccessor httpContext)
    {
        _mediator = mediator;
        _httpContext = httpContext;
    }

    [HttpGet("me", Name = "GetUserStatus")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetUserStatusOutDto), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<GetUserStatusOutDto>> GetStatus()
    {
        var id = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var query = new GetUserStatusQuery();
        query.UserId = id;
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPut("{userId}", Name = "SetUserStatusAsAdmin")]
    [Consumes("multipart/form-data")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> SetUserStatus(
    [FromRoute] long userId,
    [FromForm] SetUserStatusRequestDto dto,
    [FromServices] IValidator<SetUserStatusRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var command = dto.Adapt<SetUserStatusCommand>();
        command.UserId = userId;

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet("comments", Name = "GetStatusCommentsAsAdmin")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(IEnumerable<GetStatusCommentsOutDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IQueryable<GetStatusCommentsOutDto>>> GetStatusComments(
        [FromQuery] GridifyQuery query)
    {
        var request = query.Adapt<GetStatusCommentsQuery>();
        var result = await _mediator.Send(request);

        return Ok(result);
    }

    [HttpPost("comments", Name = "CreateStatusCommentAsAdmin")]
    [Consumes("multipart/form-data")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> CreateStatusComment(
        [FromForm] CreateStatusCommentRequestDto dto,
        [FromServices] IValidator<CreateStatusCommentRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var command = new CreateStatusCommentCommand
        {
            CommentDto = dto.Adapt<CreateStatusCommentInDto>()
        };

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("{commentId}", Name = "DeleteCommentAsAdmin")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> DeleteComment(
    [FromRoute] long commentId)
    {
        var command = new DeleteStatusCommentCommand()
        {
            CommentId = commentId.ToString()
        };

        var result = await _mediator.Send(command);

        return Ok(result);
    }
}

