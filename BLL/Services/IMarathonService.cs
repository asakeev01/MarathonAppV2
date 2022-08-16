using MarathonApp.DAL.EF;
using MarathonApp.Models.Partners;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MarathonApp.DAL.Entities;
using Models.SavedFiles;
using Microsoft.AspNetCore.Http;
using MarathonApp.DAL.Enums;
using Microsoft.AspNetCore.Hosting;
using MarathonApp.Models.Exceptions;
using System.Transactions;

namespace MarathonApp.BLL.Services
{
    public interface IMarathonService
    {
        Task<IEnumerable<MarathonModel.ListMarathon>> List();
        Task Add(MarathonModel.AddMarathon model);
        Task<MarathonModel.GetMarathon> ById(int id);
        Task Edit(MarathonModel.EditMarathon model);
        Task EditDistance(MarathonModel.EditMarathonDistance model);
        Task AddPartner(MarathonModel.AddPartner model);
        Task DeletePartner(MarathonModel.DeletePartner model);
        Task AddImage(int id, SavedFileModel.Add<IFormFile> file);
        Task DeleteImage(MarathonModel.DeleteImage model);
    }

    public class MarathonService : IMarathonService
    {
        protected MarathonContext Context { get; }
        private ISavedFileService FileService { get; }
        private IWebHostEnvironment _webHostEnvironment;

        public MarathonService(MarathonContext context, ISavedFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            Context = context;
            _webHostEnvironment = webHostEnvironment;
            FileService = fileService;
        }

        public async Task Add(MarathonModel.AddMarathon model)
        {
            var entity = model.Adapt<Marathon>();
            await Context.Set<Marathon>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MarathonModel.ListMarathon>> List()
        {
            return await Context.Marathons
                .AsNoTracking()
                .Include(x => x.Images)
                .ProjectToType<MarathonModel.ListMarathon>()
                .ToListAsync();
        }

        public async Task<MarathonModel.GetMarathon> ById(int id)
        {
            return await Context.Marathons
                .AsNoTracking()
                .Include(x => x.Partners)
                .Include(x => x.Images)
                .ProjectToType<MarathonModel.GetMarathon>()
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new HttpException("Marathon does not exists!", System.Net.HttpStatusCode.NotFound);
        }

        public async Task Edit(MarathonModel.EditMarathon model)
        {
            var entity = await Context.Marathons
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            model.Adapt(entity);
            await Context.SaveChangesAsync();
        }

        public async Task EditDistance(MarathonModel.EditMarathonDistance model)
        {
            var entity = await Context.Marathons
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            model.Adapt(entity);
            await Context.SaveChangesAsync();
        }

        public async Task AddPartner(MarathonModel.AddPartner model)
        {
            var marathon = await Context.Marathons
                .Include(x => x.Partners)
                .FirstOrDefaultAsync(x => x.Id == model.MarathonId)
                ?? throw new HttpException("Marathon does not exists!", System.Net.HttpStatusCode.NotFound);
            var partner = await Context.Partners
                .FirstOrDefaultAsync(x => x.Id == model.PartnerId)
                ?? throw new HttpException("Partner does not exists!", System.Net.HttpStatusCode.NotFound);

            marathon.Partners.Add(partner);
            await Context.SaveChangesAsync();

        }

        public async Task DeletePartner(MarathonModel.DeletePartner model)
        {
            var marathon = await Context.Marathons
                .Include(x => x.Partners)
                .FirstOrDefaultAsync(x => x.Id == model.MarathonId)
                ?? throw new HttpException("Marathon does not exists!", System.Net.HttpStatusCode.NotFound);
            var partner = await Context.Partners
                .FirstOrDefaultAsync(x => x.Id == model.PartnerId)
                ?? throw new HttpException("Partner does not exists!", System.Net.HttpStatusCode.NotFound);

            marathon.Partners.Remove(partner);
            await Context.SaveChangesAsync();
        }

        public async Task AddImage(int id, SavedFileModel.Add<IFormFile> file)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var savedFile = await FileService.UploadFile(file, FileTypeEnum.Marathons);
            var marathon = await Context.Marathons
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new HttpException("Marathon does not exists!", System.Net.HttpStatusCode.NotFound);
            marathon.Images.Add(savedFile);
            await Context.SaveChangesAsync();
            tran.Complete();
        }

        public async Task DeleteImage(MarathonModel.DeleteImage model)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var marathon = await Context.Marathons
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == model.MarathonId)
            ?? throw new HttpException("Marathon does not exists!", System.Net.HttpStatusCode.NotFound);

            var file = await Context.FindAsync<SavedFile>(model.ImageId) 
                ?? throw new HttpException("Iamge does not exists!", System.Net.HttpStatusCode.NotFound); ;
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, file.Path).Replace("/", "\\");
            if (File.Exists(filePath))
                File.Delete(filePath);
            Context.Remove(file);
            await Context.SaveChangesAsync();

            tran.Complete();
        }
    }
}
