using IdentityServiceClient.Services;
using IdentityServiceClient.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServiceClient;
public static class Setup
{
    public static void AddTokenService(this IServiceCollection services)
    {
        services.AddScoped<IUserDatabaseContext>(s => new UserDatabaseContext($"Filename=..\\IAMs.db;connection=shared"));
        services.AddScoped<IRoleDatabaseContext>(s => new RoleDatabaseContextL($"Filename=..\\IAMs.db;connection=shared"));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<ITokenService>(s => new TokenService("SomeLongClientId"));
    }
}