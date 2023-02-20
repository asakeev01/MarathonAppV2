using System;
using System.Net;
using System.Net.Mime;
using System.Xml;
using Core.UseCases.Payments.Commands.CheckPayment;
using Core.UseCases.Payments.Commands.ReceivePayment;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Payments.Dtos.Requests;

namespace WebApi.Endpoints.Payments;

[ApiController]
[Route("api/v{version:apiVersion}/payments")]
//[Consumes(MediaTypeNames.Application.Json)]
//[Produces(MediaTypeNames.Application.Json)]
public class PaymentsController : BaseController
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create Application with promocode
    /// </summary>
    /// <response code="200">Ok</response>
    [HttpPost("receive")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<HttpStatusCode>> ReceivePayment(
        [FromForm] ReceivePaymentRequestDto dto
        )
    {
        var receivePaymentCommand = new ReceivePaymentCommand()
        {
            PaymentDto = dto.Adapt<ReceivePaymentInDto>(),
        };

        var result = await _mediator.Send(receivePaymentCommand);

        return Ok(result);
    }

    [HttpPost("check")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> CheckPayment(
        [FromForm] CheckPaymentRequestDto dto
        )
    {

        var checkPaymentCommand = new CheckPaymentCommand()
        {
            PaymentDto = dto.Adapt<CheckPaymentInDto>(),
        };

        var result = await _mediator.Send(checkPaymentCommand);

        return result;
    }
}

