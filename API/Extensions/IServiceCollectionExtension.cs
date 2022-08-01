using MarathonApp.Infrastructure;

namespace MarathonApp.Extensions
{
    internal static class IServiceCollectionExtension
    {
        internal static void RegisterMapster(this IServiceCollection services)
        {
            MapsterProfile.Register();
        }
    }
}
