using CategoryService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;

public static class InfrastructureSetup
{
    public static void AddCatalogDbContext(this IServiceCollection services)
    {
        services.AddScoped<IApplicationContext, ApplicationContext>();
        services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase(databaseName: "Test"));
     }
}