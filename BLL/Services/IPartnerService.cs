using MarathonApp.DAL.EF;
using MarathonApp.Models.Partners;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MarathonApp.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using MarathonApp.Models.Exceptions;
using Models.SavedFiles;
using Microsoft.AspNetCore.Http;
using MarathonApp.DAL.Enums;
using System.Transactions;

namespace MarathonApp.BLL.Services
{
    public interface IPartnerService
    {
        Task<IEnumerable<PartnerModel.ListPartner>> List();
        Task Add(SavedFileModel.Add<IFormFile> file);
        Task <PartnerModel.Get> ById(int id);
        Task Edit(int id, SavedFileModel.Add<IFormFile> file);
        Task Delete(int id);
    }

    public class PartnerService : IPartnerService
    {
        protected MarathonContext Context { get; }
        public object AppConstants { get; private set; }
        private ISavedFileService FileService { get; }
        private IWebHostEnvironment _webHostEnvironment;
        public PartnerService(MarathonContext context, ISavedFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            Context = context;
            FileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Add(SavedFileModel.Add<IFormFile> file)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var savedFile = await FileService.UploadFile(file, FileTypeEnum.Partners);
            var entity = new Partner();
            await Context.Set<Partner>().AddAsync(entity);
            entity.ImageId = savedFile.Id;
            await Context.SaveChangesAsync();

            tran.Complete();
        }

        public async Task <IEnumerable<PartnerModel.ListPartner>> List()
        {
            return await Context.Partners
                .AsNoTracking()
                .Include(m => m.Image)
                .ProjectToType<PartnerModel.ListPartner>()
                .ToListAsync();
        }

        public async Task<PartnerModel.Get> ById(int id)
        {
            var result = await Context.Partners
                .AsNoTracking()
                .Include(m => m.Image)
                .ProjectToType<PartnerModel.Get>()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                throw new HttpException("Partner does not exists!", System.Net.HttpStatusCode.NotFound);
            return result;
        }

        public async Task Edit(int id, SavedFileModel.Add<IFormFile> newFile)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var entity = await Context.Partners
                .Include(m => m.Image)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new HttpException("Partner does not exists!", System.Net.HttpStatusCode.NotFound);
            var file = entity.Image;
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, file.Path).Replace("/", "\\");
            if (File.Exists(filePath))
                File.Delete(filePath);
            var savedFile = await FileService.UploadFile(newFile, FileTypeEnum.Partners);
            entity.ImageId = savedFile.Id;
            await Context.SaveChangesAsync();
            tran.Complete();
        }
        public async Task Delete(int id)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var entity = await Context.Partners
                .Include(m => m.Image)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new HttpException("Partner does not exists!", System.Net.HttpStatusCode.NotFound);
            var file = entity.Image;
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, file.Path).Replace("/", "\\");
            if (File.Exists(filePath))
                File.Delete(filePath);
            Context.Remove(entity);
            Context.Remove(file);

            await Context.SaveChangesAsync();

            tran.Complete();
        }
    }
}
