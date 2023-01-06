using System;
namespace Domain.Common.Options;

public class PaymentOptions
{
    public string Url { get; set; }
    public string InitPaymentUrl { get; set; }
    public string SecretKey { get; set; }
    public int MerchantId { get; set; }
}


