using Microsoft.Extensions.Hosting;

namespace ItemChangedListener;

public static class Program
{
    public static Task Main(string[] args)
    {
        return ApplicationBuilder.CreateHostBuilder(args, typeof(Startup)).Build().RunAsync();
    }
}