using IdentityServiceClient.Models;
using LiteDB;

namespace IdentityServiceClient.Storage;

public class RoleDatabaseContextL : IRoleDatabaseContext
{
    private const string CollectionName = "roles";

    private readonly string _connectionString;

    public RoleDatabaseContextL(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task Upsert(Role role)
    {
        using var db = new LiteDatabase(_connectionString);
        var collection = db.GetCollection<Role>(CollectionName);
        return Task.FromResult(collection.Upsert(role));
    }

    public Task<List<Role>> GetAll()
    {
        using var db = new LiteDatabase(_connectionString);
        var collection = db.GetCollection<Role>(CollectionName);
        return Task.FromResult(collection.FindAll().ToList());
    }
}