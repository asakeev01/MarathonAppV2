using System;
using System.Net;
using System.Net.Mime;
using Core.UseCases.Auth.Commands.ChangePassword;
using Core.UseCases.Auth.Commands.ConfirmEmail;
using Core.UseCases.Auth.Commands.Login;
using Core.UseCases.Auth.Commands.Login.Google;
using Core.UseCases.Auth.Commands.Register;
using Domain.Entities.Users.Constants;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Users.Dtos.Requests;

namespace WebApi.Endpoints.Auth
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
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

        [HttpPost("registeradmin", Name = "RegisterAdmin")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = Roles.Owner)]
        public async Task<ActionResult<HttpStatusCode>> RegisterAdminAsync(
            [FromBody] RegisterRequestDto dto,
            [FromServices] IValidator<RegisterRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<RegisterAdminCommand>();

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPost("login", Name = "Login")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(typeof(LoginOutDto),StatusCodes.Status200OK)]
        public async Task<ActionResult<HttpStatusCode>> LoginAsync(
            [FromBody] LoginRequestDto dto,
            [FromServices] IValidator<LoginRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<LoginCommand>();

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("logingoogle", Name = "LoginGoogle")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(typeof(LoginOutDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<HttpStatusCode>> LoginGoogleAsync(
            [FromBody] LoginGoogleRequestDto dto,
            [FromServices] IValidator<LoginGoogleRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<LoginRegisterGoogleCommand>();

            var result = await _mediator.Send(command);

            return Ok(result);
        }


        [HttpPost("refresh", Name = "Refresh")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(typeof(LoginOutDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<HttpStatusCode>> RefreshAsync(
            [FromBody] RefreshRequestDto dto,
            [FromServices] IValidator<RefreshRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<RefreshCommand>();

            var result = await _mediator.Send(command);

            return Ok(result);
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

        [HttpPost("sendconfirmemail", Name = "SendConfirmEmail")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HttpStatusCode>> SendConfirmEmailAsync(
            [FromBody] SendConfirmEmailRequestDto dto,
            [FromServices] IValidator<SendConfirmEmailRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<SendConfirmEmailCommand>();

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPost("forgetpassword", Name = "ForgetPassword")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HttpStatusCode>> ForgetPasswordAsync(
            [FromBody] ForgetPasswordRequestDto dto,
            [FromServices] IValidator<ForgetPasswordRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<SendPasswordResetTokenCommand>();

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPost("resetpassword", Name = "ResetPassword")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HttpStatusCode>> ResetPasswordAsync(
            [FromBody] ResetPasswordRequestDto dto,
            [FromServices] IValidator<ResetPasswordRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<ResetPasswordCommand>();

            var result = await _mediator.Send(command);

            return result;
        }

        [HttpPost("changepassword", Name = "ChangePassword")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HttpStatusCode>> ChangePasswordAsync(
            [FromBody] ChangePasswordRequestDto dto,
            [FromServices] IValidator<ChangePasswordRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = dto.Adapt<ChangePasswordCommand>();

            var result = await _mediator.Send(command);

            return result;
        }
    }
}

