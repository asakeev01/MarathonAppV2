using System;
using System.Net.Mime;
using System.Security.Claims;
using Core.UseCases.Documents.Queries.GetUserDocument;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions.ErrorHandlingServices;

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
            request.userId = userId;
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}

