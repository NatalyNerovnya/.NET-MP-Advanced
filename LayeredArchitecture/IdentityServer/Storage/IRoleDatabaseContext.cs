using IdentityServer.Models;

namespace IdentityServer.Storage;

public interface IRoleDatabaseContext
{
    Task Upsert(Role user);

    Task<List<Role>> GetAll();
}