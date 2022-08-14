using IdentityServiceClient.Models;
using IdentityServiceClient.Storage;

namespace IdentityServiceClient.Services;

public class UserService : IUserService
{
    private readonly IUserDatabaseContext _dbContext;

    public UserService(IUserDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Add(User user)
    {
        return _dbContext.Upsert(user);
    }

    public Task Update(User user)
    {
        return _dbContext.Upsert(user);
    }

    public Task<List<User>> GetAll()
    {
        return _dbContext.GetAll();
    }
}