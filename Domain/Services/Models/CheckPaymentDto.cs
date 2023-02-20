using System;
namespace Domain.Services.Models;

public class CheckPaymentDto
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

