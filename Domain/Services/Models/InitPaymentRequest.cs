using System;
namespace Domain.Services.Models;

public class InitPaymentRequest
{
    public string pg_order_id { get; set; }
    public int pg_merchant_id { get; set; }
    public int pg_amount { get; set; }
    public string pg_description { get; set; }
    public string pg_result_url { get; set; }
    public string pg_salt { get; set; }
    public string pg_sig { get; set; }
    public int pg_lifetime { get; set; }
    public string pg_user_contact_email { get; set; }
}


