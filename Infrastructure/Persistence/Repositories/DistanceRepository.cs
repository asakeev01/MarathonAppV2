using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Distances;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class DistanceRepository : BaseRepository<Distance>, IDistanceRepository
{
    public DistanceRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}
