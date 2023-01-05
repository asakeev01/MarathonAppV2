using Domain.Common.Contracts;
namespace RemoveApplicationServiceWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<IUnitOfWork>();


            var application = await context.ApplicationRepository.GetFirstOrDefaultAsync(predicate: x => x.RemovalTime <= DateTime.Now);
            if (application != null)
            {
                try
                {
                    _logger.LogInformation("Deleting application with id - {0}", application.Id);
                    await context.ApplicationRepository.Delete(application, save: true);
                    _logger.LogInformation("Application with id - {0}, deleted.", application.Id);

                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    _logger.LogInformation("Cant delete application with id - {0}", application.Id);
                }
            }
            
            context.Dispose();

            await Task.Delay(1000, stoppingToken);
        }
    }
}