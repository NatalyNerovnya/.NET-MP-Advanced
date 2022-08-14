using IdentityServiceClient.Models;

namespace IdentityServiceClient.Storage;

public interface IRoleDatabaseContext
{
    Task Upsert(Role user);

    Task<List<Role>> GetAll();
}