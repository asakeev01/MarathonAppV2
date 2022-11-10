using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Vouchers;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class PromocodeRepository : BaseRepository<Promocode>, IPromocodeRepository
{
    public PromocodeRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}
