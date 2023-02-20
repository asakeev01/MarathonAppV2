using System;
using Core.Common.Bases;
using Domain.Services.Models;

namespace Core.UseCases.Payments.Commands.CheckPayment;

public record CheckPaymentInDto : BaseDto<CheckPaymentInDto, CheckPaymentDto>
{
    public string pg_order_id { get; set; }
    public int pg_payment_id { get; set; }
    public string pg_amount { get; set; }
    public string? pg_currency { get; set; }
    public string? pg_ps_amount { get; set; }
    public string? pg_ps_full_amount { get; set; }
    public string? pg_ps_currency { get; set; }
    public string pg_salt { get; set; }
    public string pg_sig { get; set; }
}

