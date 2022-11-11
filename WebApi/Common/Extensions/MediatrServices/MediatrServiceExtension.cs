using System.Reflection;
using Core.UseCases.Users.Queries.GetUserProfile;
using MediatR;

namespace WebApi.Common.Extensions.MediatrServices;

public static class MediatrServiceExtension
{
    internal static void AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetUserProfileHandler).Assembly);
    }

}