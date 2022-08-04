using MarathonApp.DAL.EF;
using MarathonApp.Models.Partners;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MarathonApp.DAL.Entities;

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
        Task RemovePartner(MarathonModel.RemovePartner model);
    }


    public class MarathonService : IMarathonService
    {
        protected MarathonContext Context { get; }
        public MarathonService(MarathonContext context)
        {
            Context = context;
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
                .ProjectToType<MarathonModel.ListMarathon>()
                .ToListAsync();
        }

        public async Task<MarathonModel.GetMarathon> ById(int id)
        {
            return await Context.Marathons
                .AsNoTracking()
                .Include(x => x.Partners)
                .ProjectToType<MarathonModel.GetMarathon>()
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw HttpException("Marathon does not exists!", System.Net.HttpStatusCode.NotFound);
        }

        private Exception HttpException(string v, object htt)
        {
            throw new NotImplementedException();
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
                ?? throw HttpException("Marathon does not exists!", System.Net.HttpStatusCode.NotFound);
            var partner = await Context.Partners
                .FirstOrDefaultAsync(x => x.Id == model.PartnerId)
                ?? throw HttpException("Partner does not exists!", System.Net.HttpStatusCode.NotFound);

            marathon.Partners.Add(partner);
            await Context.SaveChangesAsync();

        }

        public async Task RemovePartner(MarathonModel.RemovePartner model)
        {
            var marathon = await Context.Marathons
                .Include(x => x.Partners)
                .FirstOrDefaultAsync(x => x.Id == model.MarathonId)
                ?? throw HttpException("Marathon does not exists!", System.Net.HttpStatusCode.NotFound);
            var partner = await Context.Partners
                .FirstOrDefaultAsync(x => x.Id == model.PartnerId)
                ?? throw HttpException("Partner does not exists!", System.Net.HttpStatusCode.NotFound);

            marathon.Partners.Remove(partner);
            await Context.SaveChangesAsync();
        }
    }
}