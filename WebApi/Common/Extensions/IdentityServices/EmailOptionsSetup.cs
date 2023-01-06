using System;
using Domain.Common.Options;
using Microsoft.Extensions.Options;

namespace WebApi.Common.Extensions.IdentityServices;

public class EmailOptionsSetup : IConfigureOptions<EmailOptions>
{
    private readonly IConfiguration _configuration;

    public EmailOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private const string ConfigurationSectionName = "MailSettings";

    public void Configure(EmailOptions options)
    {
        _configuration
            .GetSection(ConfigurationSectionName)
            .Bind(options);
    }
}


