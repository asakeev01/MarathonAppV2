using Domain.Common.Contracts;
using Domain.Common.Resources.SharedResource;
using Domain.Entities.SavedFiles;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories;

public class SavedFileRepository : BaseRepository<SavedFile>, ISavedFileRepository
{
    public SavedFileRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> localizer) : base(repositoryContext, localizer)
    {
    }
}
