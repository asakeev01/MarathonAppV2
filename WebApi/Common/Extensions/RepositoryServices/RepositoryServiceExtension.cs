using Domain.Common.Contracts;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Base;

namespace WebApi.Common.Extensions.RepositoryServices;

public static class RepositoryServiceExtension
{
    internal static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IMarathonRepository), typeof(MarathonRepository));
        services.AddScoped(typeof(IDistanceRepository), typeof(DistanceRepository));
        services.AddScoped(typeof(IMarathonTranslationRepository), typeof(MarathonTranslationRepository));
        services.AddScoped(typeof(ISavedFileRepository), typeof(SavedFileRepository));
        services.AddScoped(typeof(IVoucherRepository), typeof(VoucherRepository));
        services.AddScoped(typeof(IPromocodeRepository), typeof(PromocodeRepository));
    }
}