using System;
using Mapster;
using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using Models.Applications;

namespace BLL.Services
{
    public interface IApplicationService
    {
        Task ApplyAsync(ApplyModel model);
    }

    public class ApplicationService : IApplicationService
    {
        private MarathonContext _context;
        public ApplicationService(MarathonContext context)
        {
            _context = context;
        }

        public async Task ApplyAsync(ApplyModel model)
        {
            var applicationEntity = model.Adapt<Application>();

            await _context.Applications.AddAsync(applicationEntity);

            await _context.SaveChangesAsync();
        }
    }
}

