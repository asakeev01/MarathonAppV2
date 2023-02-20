using System;
using Domain.Common.Options;
using Microsoft.Extensions.Options;

namespace WebApi.Common.Extensions.IdentityServices;

public class AppUrlOptionsSetup : IConfigureOptions<AppUrlOptions>
{
    private readonly IConfiguration _configuration;

    public AppUrlOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private const string ConfigurationSectionName = "AppUrl";

    public void Configure(AppUrlOptions options)
    {
        _configuration
            .GetSection(ConfigurationSectionName)
            .Bind(options);
    }
}


