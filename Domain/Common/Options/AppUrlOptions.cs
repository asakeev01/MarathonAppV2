using System;
namespace Domain.Common.Options;

public class AppUrlOptions
{
    public string BackUrl { get; set; }
    public string FrontUrl { get; set; }
    public string PaymentUrl { get; set; }
    public string ReceivePaymentUrl { get; set; }
    public string CheckPaymentUrl { get; set; }
}


