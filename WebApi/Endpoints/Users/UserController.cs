using System;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using Core.UseCases.Users.Commands.UpdateProfile;
using Core.UseCases.Users.Queries.GetProfile;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Users.Dtos.Requests;

namespace WebApi.Endpoints.Users
{
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
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

        [HttpGet("", Name = "GetProfile")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(typeof(GetProfileOutDto), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<ActionResult<GetProfileOutDto>> GetProfile()
        {
            var email = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            var request = new GetProfileQuery();
            request.Email = email;
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("", Name = "UpdateProfile")]
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
            var updateProfileCommand = new UpdateProfileCommand()
            {
                UserDto = dto.Adapt<UpdateProfileInDto>(),
            };
            updateProfileCommand.UserDto.Email = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            var result = await _mediator.Send(updateProfileCommand);

            return Ok(result);
        }
    }
}
