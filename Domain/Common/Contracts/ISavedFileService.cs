using Domain.Common.Constants;
using Domain.Entities.SavedFiles;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces;

public interface ISavedFileService
{
    Task<SavedFile> UploadFile(IFormFile file, FileTypeEnum fileType);
    Task DeleteFile(SavedFile file);
}
