using System;
using Domain.Common.Options;
using Microsoft.Extensions.Options;

namespace WebApi.Common.Extensions.IdentityServices;

public class GoogleAuthOptionsSetup : IConfigureOptions<GoogleAuthOptions>
{
    private readonly IConfiguration _configuration;

    public GoogleAuthOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private const string ConfigurationSectionName = "GoogleAuthSettings";

    public void Configure(GoogleAuthOptions options)
    {
        _configuration
            .GetSection(ConfigurationSectionName)
            .Bind(options);
    }
}


