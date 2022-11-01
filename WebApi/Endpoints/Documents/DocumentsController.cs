using System;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using Core.UseCases.Documents.Commands.DeleteUserDocument;
using Core.UseCases.Documents.Commands.UploadUserDocument;
using Core.UseCases.Documents.Queries.GetUserDocument;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Documents.Dtos.Requests;

namespace WebApi.Endpoints.Documents
{
    [ApiController]
    [Route("api/v{version:apiVersion}/documents")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class DocumentsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContext;

        public DocumentsController(IMediator mediator, IHttpContextAccessor httpContext)
        {
            _mediator = mediator;
            _httpContext = httpContext;
        }

        [HttpGet("", Name = "GetUserDocument")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(typeof(GetUserDocumentOutDto), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<ActionResult<GetUserDocumentOutDto>> GetProfile()
        {
            var userId = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var request = new GetUserDocumentQuery();
            request.UserId = userId;
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("", Name = "UploadUserDocument")]
        [Consumes("multipart/form-data")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<ActionResult<HttpStatusCode>> UploadDocument(
            [FromForm] UploadUserDocumentRequestDto dto,
            [FromServices] IValidator<UploadUserDocumentRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);

            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = new UploadUserDocumentCommand()
            {
                UserId = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                Document = dto.Document,
                DocumentType = dto.DocumentType
            };

            var result = await _mediator.Send(command);

            return Ok(result);

        }

        [HttpDelete("", Name = "DeleteUserDocument")]
        [Consumes("multipart/form-data")]
        [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
        [ProducesResponseType(typeof(HttpStatusCode), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<ActionResult<HttpStatusCode>> DeleteDocument(
            [FromForm] DeleteUserDocumentRequestDto dto,
            [FromServices] IValidator<DeleteUserDocumentRequestDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);

            if (!validation.IsValid)
            {
                return validation.ToBadRequest();
            }

            var command = new DeleteUserDocumentCommand()
            {
                UserId = _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                DocumentType = dto.DocumentType
            };

            var result = await _mediator.Send(command);

            return Ok(result);

        }
    }
}

