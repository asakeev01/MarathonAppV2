using MarathonApp.DAL.EF;
using MarathonApp.DAL.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Models.SavedFiles;

namespace MarathonApp.BLL.Services
{
    public interface ISavedFileService
    {
        Task<SavedFile> UploadFile(SavedFileModel.Add<IFormFile> model, FileTypeEnum fileType);
    }

    public class SavedFileService : ISavedFileService
    {
        private MarathonContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public SavedFileService(MarathonContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<SavedFile> UploadFile(SavedFileModel.Add<IFormFile> model, FileTypeEnum fileType)
        {
            var file = model.File;

            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "staticfiles", fileType.ToString());
            string filePath = Path.Combine(directoryPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string databasePath = "staticfiles/" + fileType + '/' + file.FileName;
            var dbFile = new SavedFile();
            dbFile.Path = databasePath;
            dbFile.Name = file.FileName;
            var db = await _context.Set<SavedFile>().AddAsync(dbFile);
            await _context.SaveChangesAsync();
            return db.Entity; 
        }
    }
}

