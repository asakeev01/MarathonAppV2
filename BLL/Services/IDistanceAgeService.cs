using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using MarathonApp.Models.Exceptions;

namespace MarathonApp.BLL.Services
{
    public interface IDistanceAgeService
    {
        Task Delete(int id);
    }

    public class DistanceAgeService : IDistanceAgeService
    {
        protected MarathonContext Context { get; }
        public DistanceAgeService(MarathonContext context)
        {
            Context = context;
        }
        public async Task Delete(int id)
        {
            var entity = await Context.FindAsync<DistanceAge>(id);
            if (entity == null)
                throw new HttpException("DistanceAge does not exists!", System.Net.HttpStatusCode.NotFound);
            Context.Remove(entity);

            await Context.SaveChangesAsync();
        }
    }
}
