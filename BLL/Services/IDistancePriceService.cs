using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Exceptions;

namespace MarathonApp.BLL.Services
{
    public interface IDistancePriceService
    {
        Task Delete(int id);
    }

    public class DistancePriceService : IDistancePriceService
    {
        protected MarathonContext Context { get; }
        public DistancePriceService(MarathonContext context)
        {
            Context = context;
        }
        public async Task Delete(int id)
        {
            var entity = await Context.FindAsync<DistancePrice>(id);
            if (entity == null)
                throw new HttpException("DistancePrice does not exists!", System.Net.HttpStatusCode.NotFound);
            Context.Remove(entity);

            await Context.SaveChangesAsync();
        }
    }
}
