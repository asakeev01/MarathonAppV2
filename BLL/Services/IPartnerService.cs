using MarathonApp.DAL.EF;
using MarathonApp.Models.Partners;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MarathonApp.DAL.Entities;
using Microsoft.AspNetCore.Hosting;

namespace MarathonApp.BLL.Services
{
    public interface IPartnerService
    {
        Task<IEnumerable<PartnerModel.ListPartner>> List();
        Task Add(PartnerModel.AddPartner model, (Stream Source, string FileName) file);
        Task <PartnerModel.Get> ById(int id);
        Task Edit(PartnerModel.Edit model);
        //Task AddImage(PartnerModel.AddPartner model, (Stream Source, string FileName) file);
    }


    public class PartnerService : IPartnerService
    {
        protected MarathonContext Context { get; }
        public object AppConstants { get; private set; }

        private IWebHostEnvironment _webHostEnvironment;
        public PartnerService(MarathonContext context, IWebHostEnvironment webHostEnvironment)
        {
            Context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Add(PartnerModel.AddPartner model, (Stream Source, string FileName) file)
        {
            var entity = model.Adapt<Partner>();
            await Context.Set<Partner>().AddAsync(entity);
            //var savedFile = await AttachFile(entity, file);
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
            return await Context.Partners
                .AsNoTracking()
                .ProjectToType<PartnerModel.Get>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Edit(PartnerModel.Edit model)
        {
            var entity = await Context.Partners
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            model.Adapt(entity);
            await Context.SaveChangesAsync();
        }

        //protected async Task<SavedFile> AttachFile(Partner entity, (Stream Source, string FileName) file)
        //{

        //}
    }
}