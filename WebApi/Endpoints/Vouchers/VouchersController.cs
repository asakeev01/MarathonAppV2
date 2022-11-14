using System.Net;
using System.Net.Mime;
using Core.UseCases.Marathons.Commands.CraeteMarathon;
using Core.UseCases.Marathons.Commands.CreateMarathon;
using Core.UseCases.Marathons.Commands.PutMarathon;
using Core.UseCases.Marathons.Queries.GetMarathon;
using Core.UseCases.Marathons.Queries.GetMarathonAdmin;
using Core.UseCases.Marathons.Queries.GetMarathons;
using Core.UseCases.Vouchers.Commands.AddPromocodesToVoucher;
using Core.UseCases.Vouchers.Commands.CreateVoucher;
using Core.UseCases.Vouchers.Queries.GenerateExcelPromocodes;
using Core.UseCases.Vouchers.Queries.GetVouchers;
using FluentValidation;
using Gridify;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Extensions;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Endpoints.Marathons.Dtos.Requests;
using WebApi.Endpoints.Vouchers.Dtos.Requests;

namespace WebApi.Endpoints.Distances;

[ApiController]
[Route("api/v{version:apiVersion}/vouchers")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class VouchersController : BaseController
{
    private readonly IMediator _mediator;

    public VouchersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get Vouchers
    /// </summary>
    /// <response code="200"></response>
    [HttpGet("")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetPromocodesByVaucherIdQueryOutDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetPromocodesByVaucherIdQueryOutDto>> GetVouchers(
        [FromQuery] GridifyQuery query
        )
    {
        var getVourchersQuery = new GetVourchersQuery()
        {
            Query = query,
        };

        var result = await _mediator.Send(getVourchersQuery);

        return Ok(result);
    }

    /// <summary>
    /// Get Voucher`s Promocodes by id
    /// </summary>
    /// <response code="200"></response>
    [HttpGet("{voucherId:int}/promocodes")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetPromocodesByVaucherIdQueryOutDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetPromocodesByVaucherIdQueryOutDto>> GetVoucherPromocodes(
        [FromRoute] int voucherId,
        [FromQuery] GridifyQuery query)
    {
        var getVourcherByIdQuery = new GetPromocodesByVaucherIdQuery()
        {
            VoucherId = voucherId,
            Query = query,
        };

        var result = await _mediator.Send(getVourcherByIdQuery);

        return Ok(result);
    }


    /// <summary>
    /// Create Voucher
    /// </summary>
    /// <response code="200"></response>
    [HttpPost("")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(typeof(GetPromocodesByVaucherIdQueryOutDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> CreateVoucher(
        [FromBody] CreateVoucherRequestDto dto,
        [FromServices] IValidator<CreateVoucherRequestDto> validator
        )
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }

        var createVoucherCommand = new CreateVoucherCommand()
        {
            VoucherDto = dto.Adapt<CreateVoucherInDto>(),
        };

        var result = await _mediator.Send(createVoucherCommand);

        return Ok(result);
    }

    /// <summary>
    /// Add Promocodes to Voucher
    /// </summary>
    /// <response code="200"></response>
    [HttpPost("{voucherId}/promocodes")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType( StatusCodes.Status200OK)]
    public async Task<ActionResult> AddPromocodesToVoucher(
        [FromRoute] int voucherId,
        [FromBody] AddPromocodesToVoucherRequestDto dto,
        [FromServices] IValidator<AddPromocodesToVoucherRequestDto> validator
        )
    {
        var validation = await validator.ValidateAsync(dto);

        if (!validation.IsValid)
        {
            return validation.ToBadRequest();
        }

        var addPromocodesToVoucherCommand = new AddPromocodesToVoucherCommand()
        {
            VoucherId = voucherId,
            PromocodesDto = dto.Adapt<AddPromocodesToVoucherCommandInDto>(),
        };

        var result = await _mediator.Send(addPromocodesToVoucherCommand);

        return Ok(result);
    }

    /// <summary>
    /// Export Vouchers to excel
    /// </summary>
    [HttpGet("{voucherId}/excel")]
    [ProducesDefaultResponseType(typeof(CustomProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GenerateExcelPromocodes(
        [FromRoute] int voucherId
        )
    {
        var generateExcelPromocodes = new GenerateExcelPromocodesQuery()
        {
            VoucherId = voucherId,
        };

        var (result, voucherName) = await _mediator.Send(generateExcelPromocodes);
        HttpContext.Response.Headers.Add("content-disposition", $"attachment; filename=Promocodes_{voucherName}_{DateTime.Now.ToString("dd/MM/yyyy")}.xlsx");
        this.Response.ContentType = "application/vnd.ms-excel";
        return File(result, "application/vnd.ms-excel");
    }
}