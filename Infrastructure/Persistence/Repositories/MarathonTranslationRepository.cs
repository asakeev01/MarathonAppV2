using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Marathons;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class MarathonTranslationRepository : BaseRepository<MarathonTranslation>, IMarathonTranslationRepository
{
    public MarathonTranslationRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}
