using EmailServiceWorker;
using EmailServiceWorker.Options;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.ConfigureOptions<EmailOptionsSetup>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
