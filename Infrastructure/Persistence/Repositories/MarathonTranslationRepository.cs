using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.Marathons;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class MarathonTranslationRepository : BaseRepository<MarathonTranslation>, IMarathonTranslationRepository
    {
        public MarathonTranslationRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
        {
        }
    }
}
