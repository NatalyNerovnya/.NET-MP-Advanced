using IdentityServer.Models;

namespace IdentityServer.Storage;

public interface IUserDatabaseContext
{
    Task Upsert(User user);
    Task<List<User>> GetAll();
}