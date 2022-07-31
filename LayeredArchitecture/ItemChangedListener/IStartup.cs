using Microsoft.Extensions.DependencyInjection;

namespace ItemChangedListener;

public interface IStartup
{
    void ConfigureServices(IServiceCollection services);
}