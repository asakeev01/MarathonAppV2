using FluentValidation;
using Microsoft.Extensions.FileProviders;
﻿using Gridify;
using Serilog;
using Serilog.Core;
using WebApi.Common.Extensions.ApiVersioningServices;
using WebApi.Common.Extensions.DomainServices;
using WebApi.Common.Extensions.EfServices;
using WebApi.Common.Extensions.ErrorHandlingServices;
using WebApi.Common.Extensions.FluentValidationServices;
using WebApi.Common.Extensions.GridifyServices;
using WebApi.Common.Extensions.IdentityServices;
using WebApi.Common.Extensions.LocalizationServices;
using WebApi.Common.Extensions.MapsterServices;
using WebApi.Common.Extensions.MediatrServices;
using WebApi.Common.Extensions.RepositoryServices;
using WebApi.Common.Extensions.SwaggerServices;
using static WebApi.Common.Extensions.FluentValidationServices.FluentValidationServiceExtension;

namespace WebApi.Common.Extensions;

public static class WebApplicationBuilderExtension
{
    internal static void ConfigureServices(this WebApplicationBuilder builder, Logger logger)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        var env = builder.Environment;
        
        services.AddMapster();
        services.AddFluentValidators();
        services.AddApiVersion();
        services.AddSwagger();
        services.AddGridify(configuration);
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddLocalizationService();
        services.AddErrorHandlingService(configuration, env, logger);
        services.AddMediatr();
        services.AddAppDbContext(configuration, env);
        services.AddIdentityService(configuration);
        services.AddRepositories();
        services.RegisterDomainServices(configuration);
        ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();

    }

    internal static async Task ConfigureApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var configuration = builder.Configuration;

        app.UseErrorHandling();
        if (builder.Environment.IsDevelopment())
        {
            app.UseSwaggerUi();
        }
        app.UseRouting();
        app.UseLocalization();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        var dir = Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration.GetSection("FileSettings:PhysicalPath").Value);
        var requestPath = builder.Configuration.GetSection("FileSettings:RequestPath").Value;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new PhysicalFileProvider(dir),
            RequestPath = new PathString(requestPath),
        });

        ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();
        app.AutoMigrateDb();
        await app.Seed();
        await app.RunAsync();
    }
}