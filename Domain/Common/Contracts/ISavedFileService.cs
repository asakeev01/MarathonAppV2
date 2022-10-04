using Domain.Common.Constants;
using Domain.Entities.SavedFiles;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Services.Interfaces
{
    public interface ISavedFileService
    {
        Task<SavedFile> UploadFile(IFormFile file, FileTypeEnum fileType);
        Task DeleteFile(SavedFile file);
    }
}
