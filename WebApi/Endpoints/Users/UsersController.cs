using System;
using System.Net;
using System.Net.Mime;
using Core.UseCases.Auth.Commands.ConfirmEmail;
using Core.UseCases.Auth.Commands.RegisterUser;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Users.Dtos.Requests;

namespace WebApi.Endpoints.Users
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register", Name = "RegisterUser")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<HttpStatusCode>> RegisterUserAsync(
            [FromBody] RegisterRequestDto dto,
            [FromServices] IValidator<RegisterRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<RegisterUserCommand>();

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPost("confirmemail", Name = "ConfirmEmail")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HttpStatusCode>> RegisterAsync(
            [FromBody] ConfirmEmailRequestDto dto,
            [FromServices] IValidator<ConfirmEmailRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<ConfirmEmailCommand>();

            var result = await _mediator.Send(command);

            return result;
        }
    }
}

