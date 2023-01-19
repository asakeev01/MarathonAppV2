using System;
namespace RemoveApplicationServiceWorker.Options;

public class DeletePaymentOptions
{
    public string Url { get; set; }
    public string InitPaymentUrl { get; set; }
    public string DeletePaymentUrl { get; set; }
    public string SecretKey { get; set; }
    public string MerchantId { get; set; }
}

