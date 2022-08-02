using MarathonApp.DAL.EF;
using MarathonApp.Models.Partners;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MarathonApp.DAL.Entities;

namespace MarathonApp.BLL.Services
{
    public interface IPartnerService
    {
        Task<IEnumerable<PartnerModel.ListPartner>> List();
        Task Add(PartnerModel.AddPartner model);
        Task <PartnerModel.Get> ById(int id);
        Task Edit(PartnerModel.Edit model);
    }


    public class PartnerService : IPartnerService
    {
        protected MarathonContext Context { get; }
        public PartnerService(MarathonContext context)
        {
            Context = context;
        }

        public async Task Add(PartnerModel.AddPartner model)
        {
            var entity = model.Adapt<Partner>();
            await Context.Set<Partner>().AddAsync(entity);
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
    }
}