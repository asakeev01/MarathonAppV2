using Domain.Common.Contracts;
using Domain.Services;
using Domain.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace WebApi.Common.Extensions.DomainServices;

public static class DomainServicesExtension
{
    internal static void RegisterDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ISavedFileService, SavedFileService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        services.AddScoped<ISavedDocumentService, SavedDocumentService>();
    }
}