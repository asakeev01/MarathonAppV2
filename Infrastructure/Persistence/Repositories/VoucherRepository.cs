using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Vouchers;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
{
    public VoucherRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}
