using IdentityServer.Models;

namespace IdentityServer.Services;

public interface IUserService
{
    Task Add(User user);

    Task Update(User user);

    Task<List<User>> GetAll();
}