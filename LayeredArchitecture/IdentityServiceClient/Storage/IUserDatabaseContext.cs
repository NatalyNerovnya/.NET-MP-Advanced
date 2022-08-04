using IdentityServiceClient.Models;

namespace IdentityServiceClient.Storage;

public interface IUserDatabaseContext
{
    Task Upsert(User user);
    Task<List<User>> GetAll();
}