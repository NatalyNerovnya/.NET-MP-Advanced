using Microsoft.Extensions.Hosting;

namespace ItemChangedListener;

public static class ApplicationBuilder
{
    public static IHostBuilder CreateHostBuilder(string[] args, Type type)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(
                (context, collection) =>
                {
                    var instance = Activator.CreateInstance(type);
                    if (instance is IStartup startup)
                    {
                        startup.ConfigureServices(collection);
                    }
                });
    }
}