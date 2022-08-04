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

namespace MarathonApp.BLL.Services
{
    public interface IPartnerService
    {
        Task<IEnumerable<PartnerModel.ListPartner>> List();
        Task Add(PartnerModel.AddPartner model, SavedFileModel.Add<IFormFile> file);
        Task <PartnerModel.Get> ById(int id);
        Task Edit(PartnerModel.Edit model);
        Task AttachFile(Partner entity, SavedFileModel.Add<IFormFile> file);
    }


    public class PartnerService : IPartnerService
    {
        protected MarathonContext Context { get; }
        public object AppConstants { get; private set; }
        private ISavedFileService FileService { get; }
        public PartnerService(MarathonContext context, ISavedFileService fileService)
        {
            Context = context;
            FileService = fileService;  
        }

        public async Task Add(PartnerModel.AddPartner model, SavedFileModel.Add<IFormFile> file)
        {
            var savedFile = await FileService.UploadFile(file, FileTypeEnum.Partners);
            var entity = model.Adapt<Partner>();
            await Context.Set<Partner>().AddAsync(entity);
            entity.ImageId = savedFile.Id;
            //await AttachFile(entity, file);
            await Context.SaveChangesAsync();
        }

        public async Task <IEnumerable<PartnerModel.ListPartner>> List()
        {
            return await Context.Partners
                .AsNoTracking()
                .ProjectToType<PartnerModel.ListPartner>()
                .ToListAsync();
        }

        public async Task<PartnerModel.Get> ById(int id)
        {
            var result = await Context.Partners
                .AsNoTracking()
                .ProjectToType<PartnerModel.Get>()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                throw new HttpException("Partner does not exists!", System.Net.HttpStatusCode.NotFound);
            return result;
        }

        public async Task Edit(PartnerModel.Edit model)
        {
            var entity = await Context.Partners
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (entity == null)
                throw new HttpException("Partner does not exists!", System.Net.HttpStatusCode.NotFound);
            model.Adapt(entity);
            await Context.SaveChangesAsync();
        }

        public async Task AttachFile(Partner entity, SavedFileModel.Add<IFormFile> file)
        {
            var savedFile = await FileService.UploadFile(file, FileTypeEnum.Partners);
            entity.ImageId = savedFile.Id;
        }
    }
}