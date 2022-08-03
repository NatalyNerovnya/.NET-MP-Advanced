using IdentityServer.Models;
using IdentityServer.Storage;

namespace IdentityServer.Services;

public class RoleService : IRoleService
{
    private readonly IRoleDatabaseContext _databaseContext;

    public RoleService(IRoleDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Task Add(Role role)
    {
        return _databaseContext.Upsert(role);
    }

    public Task Update(Role role)
    {
        return _databaseContext.Upsert(role);
    }

    public Task<List<Role>> GetAll()
    {
        return _databaseContext.GetAll();
    }
}