using System;
using Domain.Common.Options;
using Microsoft.Extensions.Options;

namespace WebApi.Common.Extensions.PaymentServices;

public class PaymentOptionsSetup : IConfigureOptions<PaymentOptions>
{
    private readonly IConfiguration _configuration;

    public PaymentOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private const string ConfigurationSectionName = "PayBoxSettings";

    public void Configure(PaymentOptions options)
    {
        _configuration
            .GetSection(ConfigurationSectionName)
            .Bind(options);
    }
}


