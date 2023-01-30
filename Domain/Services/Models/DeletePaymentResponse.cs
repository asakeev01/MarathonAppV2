using System;
using System.Xml.Serialization;

namespace Domain.Services.Models;

[XmlRoot("response")]
public class DeletePaymentResponse
{
    public string? pg_status { get; set; }
    public int? pg_error_code { get; set; }
    public string? pg_error_description { get; set; }
    public string? pg_salt { get; set; }
    public string? pg_sig { get; set; }
}

