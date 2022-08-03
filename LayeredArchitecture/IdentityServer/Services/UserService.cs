using IdentityServer.Models;
using IdentityServer.Services.Interfaces;
using IdentityServer.Storage;

namespace IdentityServer.Services;

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