using System;
namespace Domain.Services.Models;

public class DeletePaymentRequest
{
    public string pg_merchant_id { get; set; }
    public int? pg_payment_id { get; set; }
    public string pg_salt { get; set; }
    public string pg_sig { get; set; }
}

