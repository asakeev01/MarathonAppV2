using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace WebApi.Common.Extensions.CorsServices

{
    public static class CorsServiceExtension
    {
        internal static void AddCorsExt(this IServiceCollection services)
        {
            services.AddCors(x => x.AddDefaultPolicy(b => b
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()));
        }
        internal static void UseCorsExt(this IApplicationBuilder app)
        {
            app.UseCors();
        }
    }
}

