using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Exceptions;

namespace MarathonApp.BLL.Services
{
    public interface IDistanceService
    {
        Task Delete(int id);
    }

    public class DistanceService : IDistanceService
    {
        protected MarathonContext Context { get; }
        public DistanceService(MarathonContext context)
        {
            Context = context;
        }
        public async Task Delete(int id)
        {
            var entity = await Context.FindAsync<Distance>(id);
            if (entity == null)
                throw new HttpException("Distance does not exists!", System.Net.HttpStatusCode.NotFound);
            Context.Remove(entity);

            await Context.SaveChangesAsync();
        }
    }
}
