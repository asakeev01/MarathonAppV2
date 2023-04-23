using System;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using Core.UseCases.Applications.Queries.GenerateExcelApplications;
using Core.UseCases.Users.Commands.DeleteAdminAsOwner;
using Core.UseCases.Users.Commands.UpdateUserAsAdmin;
using Core.UseCases.Users.Commands.UpdateUserProfile;
using Core.UseCases.Users.Queries.GetAdminsAsOwner;
using Core.UseCases.Users.Queries.GetUserAsAdmin;
using Core.UseCases.Users.Queries.GetUserProfile;
using Core.UseCases.Users.Queries.GetUsersAsAdmin;
using Core.UseCases.Users.Queries.GetUsersExcel;
using Domain.Entities.Users.Constants;
using FluentValidation;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Users.Dtos.Requests;

namespace WebApi.Endpoints.Users;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContext;

    public UsersController(IMediator mediator, IHttpContextAccessor httpContext)
    {
        _mediator = mediator;
        _httpContext = httpContext;
    }

    [HttpGet("me", Name = "GetUserProfile")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetUserProfileOutDto), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<GetUserProfileOutDto>> GetProfile()
    {
        var id = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var query = new GetUserProfileQuery();
        query.Id = id;
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPut("me", Name = "UpdateUserProfile")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<HttpStatusCode>> UpdateProfile(
        [FromBody] UpdateProfileRequestDto dto,
        [FromServices] IValidator<UpdateProfileRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var command = new UpdateUserProfileCommand()
        {
            UserDto = dto.Adapt<UpdateUserProfileInDto>(),
        };
        command.UserDto.Email = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        command.UserDto.Id = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet("", Name = "GetUsersAsAdmin")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(IEnumerable<GetUsersOutDto>), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin)]
    public async Task<ActionResult<IQueryable<GetUsersOutDto>>> GetUsers(
        [FromQuery] GridifyQuery query)
    {
        var getUsersQuery = new GetUsersQuery()
        {
            Query = query,
            LanguageCode = this.Request.Headers["Accept-Language"],
        };

        var result = await _mediator.Send(getUsersQuery);

        return Ok(result);
    }

    [HttpGet("{userId}", Name = "GetUserAsAdmin")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetUserOutDto), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin)]
    public async Task<ActionResult<GetUserProfileOutDto>> GetUser(
        [FromRoute] long userId)
    {
        var query = new GetUserQuery()
        {
            Id = userId
        };
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPut("{userId}", Name = "UpdateUserAsAdmin")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner + "," + Roles.Admin)]
    public async Task<ActionResult<HttpStatusCode>> UpdateUser(
        [FromRoute] long userId,
        [FromBody] UpdateUserRequestDto dto,
        [FromServices] IValidator<UpdateUserRequestDto> validator)
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }
        var command = new UpdateUserCommand()
        {
            UserDto = dto.Adapt<UpdateUserInDto>(),
        };
        command.UserDto.Id = userId.ToString();

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet("admins", Name = "GetAdminsAsOwner")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(IEnumerable<GetUsersOutDto>), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner)]
    public async Task<ActionResult<IQueryable<GetUsersOutDto>>> GetAdmins(
    [FromQuery] GridifyQuery query)
    {
        var getAdminsQuery = new GetAdminsQuery()
        {
            Query = query,
        };

        var result = await _mediator.Send(getAdminsQuery);

        return Ok(result);
    }

    [HttpDelete("{userId}", Name = "DeleteAdminAsOwner")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetUserOutDto), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Owner)]
    public async Task<ActionResult<GetUserProfileOutDto>> DeleteAdmin(
    [FromRoute] long userId)
    {
        var command = new DeleteAdminCommand()
        {
            UserId = userId,
        };
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// Export Users to excel
    /// </summary>
    [HttpGet("excel")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //[Authorize(Roles = Roles.Owner + "," + Roles.Admin)]
    public async Task<ActionResult> GenerateExcelPromocodes()
    {
        var getUsersExcelQuery = new GetUsersExcelQuery();

        var result = await _mediator.Send(getUsersExcelQuery);
        HttpContext.Response.Headers.Add("content-disposition", $"attachment; filename=Users_{DateTime.Now.ToString("dd/MM/yyyy")}.xlsx");
        HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
        this.Response.ContentType = "application/vnd.ms-excel";
        return File(result, "application/vnd.ms-excel");
    }
}

