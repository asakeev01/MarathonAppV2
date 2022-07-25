using MarathonApp.DAL.EF;
using MarathonApp.DAL.Models.Partner;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MarathonApp.DAL.Entities;

namespace MarathonApp.BLL.Services
{
    public interface IPartnerService
    {
        Task<IEnumerable<PartnerDto.List>> List();
        Task Add(PartnerDto.Add model);
        Task <PartnerDto.Get> ById(int id);
        Task Edit(PartnerDto.Edit model);
    }


    public class PartnerService : IPartnerService
    {
        protected MarathonContext Context { get; }
        public PartnerService(MarathonContext context)
        {
            Context = context;
        }

        public async Task Add(PartnerDto.Add model)
        {
            var entity = model.Adapt<Partner>();
            await Context.Set<Partner>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task <IEnumerable<PartnerDto.List>> List()
        {
            return await Context.Partners
                .AsNoTracking()
                .ProjectToType<PartnerDto.List>()
                .ToListAsync();
        }

        public async Task<PartnerDto.Get> ById(int id)
        {
            return await Context.Partners
                .AsNoTracking()
                .ProjectToType<PartnerDto.Get>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Edit(PartnerDto.Edit model)
        {
            var entity = await Context.Partners
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            model.Adapt(entity);
            await Context.SaveChangesAsync();
        }
    }
}