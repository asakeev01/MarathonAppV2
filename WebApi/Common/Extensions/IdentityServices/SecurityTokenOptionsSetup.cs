using System;
using Domain.Common.Options;
using Microsoft.Extensions.Options;

namespace WebApi.Common.Extensions.IdentityServices;

public class SecurityTokenOptionsSetup : IConfigureOptions<SecurityTokenOptions>
{
    private readonly IConfiguration _configuration;

    public SecurityTokenOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private const string ConfigurationSectionName = "Identity:JwtConfig";

    public void Configure(SecurityTokenOptions options)
    {
        _configuration
            .GetSection(ConfigurationSectionName)
            .Bind(options);
    }
}


