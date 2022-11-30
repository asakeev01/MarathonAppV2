using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Marathons;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class MarathonRepository : BaseRepository<Marathon>, IMarathonRepository
{
    public MarathonRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}