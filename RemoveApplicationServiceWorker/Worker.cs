using System.Security.Cryptography;
using System.Text;
using Domain.Common.Contracts;
using Domain.Entities.Applications.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RemoveApplicationServiceWorker.Models;
using RemoveApplicationServiceWorker.Options;

namespace RemoveApplicationServiceWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private DeletePaymentOptions _paymentOptions;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IOptionsMonitor<DeletePaymentOptions> paymentOptions)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _paymentOptions = paymentOptions.CurrentValue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<IUnitOfWork>();

            using (var transaction = await context.BeginTransactionAsync(null))
            {
                try
                {
                    var today = DateTime.Now;
                    var application = await context.ApplicationRepository.GetFirstOrDefaultAsync(predicate: x => x.RemovalTime <= today, include: source => source
                    .Include(a => a.Distance));
                    if (application != null)
                    {
                        var distance = application.Distance;
                        _logger.LogInformation("Deleting application with id - {0}", application.Id);
                        await context.ApplicationRepository.Delete(application, save: true);
                        _logger.LogInformation("Application with id - {0}, deleted.", application.Id);
                        _logger.LogInformation("Release place from - {0}", distance.Name);
                        distance.InitializedPlaces -= 1;
                        await context.DistanceRepository.Update(distance, save: true);
                        _logger.LogInformation($"DistanceId = {distance.Id}; InitializedPlaces -= 1");
                        _logger.LogInformation("Place from - {0}, released", distance.Name);
                    }
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
                context.Dispose();
                await Task.Delay(60000, stoppingToken);
        }
    }
}