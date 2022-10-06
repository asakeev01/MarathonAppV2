using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Marathons;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class PartnerRepository : BaseRepository<Partner>, IPartnerRepository
{
    public PartnerRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}
