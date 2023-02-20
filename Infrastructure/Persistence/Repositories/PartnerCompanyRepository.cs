using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Marathons;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class PartnerCompanyRepository : BaseRepository<PartnerCompany>, IPartnerCompanyRepository
{
    public PartnerCompanyRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}
