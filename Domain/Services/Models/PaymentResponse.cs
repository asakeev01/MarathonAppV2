using System;
using System.Xml.Serialization;

namespace Domain.Services.Models;

[XmlRoot("response")]
public class PaymentResponse
{
    public string pg_status { get; set; }
    public int pg_payment_id { get; set; }
    public string pg_redirect_url { get; set; }
    public string pg_redirect_url_type { get; set; }
    public string pg_salt { get; set; }
    public string pg_sig { get; set; }
}


