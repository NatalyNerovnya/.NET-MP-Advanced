using IdentityServer.Models;
using LiteDB;

namespace IdentityServer.Storage;

public class UserDatabaseContext : IUserDatabaseContext
{
    private const string CollectionName = "users";

    private readonly string _connectionString;

    public UserDatabaseContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task Upsert(User user)
    {
        using var db = new LiteDatabase(_connectionString);
        var collection = db.GetCollection<User>(CollectionName);
        return Task.FromResult(collection.Upsert(user));
    }

    public Task<List<User>> GetAll()
    {
        using var db = new LiteDatabase(_connectionString);
        var collection = db.GetCollection<User>(CollectionName);
        return Task.FromResult(collection.FindAll().ToList());
    }
}