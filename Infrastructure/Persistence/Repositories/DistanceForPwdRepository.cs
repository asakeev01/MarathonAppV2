using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;
using Domain.Entities.Distances;

namespace Infrastructure.Persistence.Repositories;

public class DistanceForPwdRepository : BaseRepository<DistanceForPWD>, IDistanceForPwdRepository
{
    public DistanceForPwdRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}