using System;
using Domain.Common.Contracts;
using Domain.Common.Resources;
using Domain.Entities.Users;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        private AppDbContext _repositoryContext;

        public RefreshTokenRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
        {
            _repositoryContext = repositoryContext;
        }
    }
}

