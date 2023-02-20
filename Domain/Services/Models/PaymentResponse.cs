using System;
using System.Xml.Serialization;

namespace Domain.Services.Models;

[XmlRoot("response")]
public class PaymentResponse
{
    public string pg_status { get; set; }
    public string pg_description { get; set; }
    public string pg_salt { get; set; }
    public string? pg_sig { get; set; }
}

