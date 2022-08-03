using IdentityServer.Models;

namespace IdentityServer.Services;

public interface IRoleService
{
    Task Add(Role role);

    Task Update(Role role);

    Task<List<Role>> GetAll();
}