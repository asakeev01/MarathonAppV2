using System;
using Microsoft.Extensions.Options;

namespace RemoveApplicationServiceWorker.Options;

public class DeletePaymentOptionsSetup : IConfigureOptions<DeletePaymentOptions>
{
    private readonly IConfiguration _configuration;

    public DeletePaymentOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private const string ConfigurationSectionName = "PayBoxSettings";

    public void Configure(DeletePaymentOptions options)
    {
        _configuration
            .GetSection(ConfigurationSectionName)
            .Bind(options);
    }
}

