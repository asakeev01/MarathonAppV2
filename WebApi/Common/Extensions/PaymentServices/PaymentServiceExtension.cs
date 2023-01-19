using System;
namespace WebApi.Common.Extensions.PaymentServices;

public static class PaymentServiceExtension
{
    internal static void AddPaymentService(this IServiceCollection services)
    {
        services.ConfigureOptions<PaymentOptionsSetup>();
    }
}


