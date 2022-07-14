using CartingService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationClient;
using NotificationClient.Interfaces;

namespace ItemChangedListener;

public class Startup : IStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddListener();
        services.AddCartService();
        services.AddScoped<IHandler, ItemChangedHandler>();

        ConfigureOptions(services);
    }

    private void ConfigureOptions(IServiceCollection services)
    {
        services.Configure<HostOptions>(
            options =>
            {
                options.ShutdownTimeout = TimeSpan.FromSeconds(60);
            });
    }
}
