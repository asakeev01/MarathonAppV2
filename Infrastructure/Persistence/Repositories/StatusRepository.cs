using System;
using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Users;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
        {
        }
    }
}

