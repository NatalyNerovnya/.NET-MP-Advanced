using CartingService.Entities.Models;
using LiteDB;

namespace CartingService.DAL;

public class CartDatabaseContext : ICartDatabaseContext
{
    private const string CartCollectionName = "carts";

    private readonly string _connectionString;

    public CartDatabaseContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task<Cart?> GetById(int id)
    {
        using var db = new LiteDatabase(_connectionString);
        var collection = db.GetCollection<Cart?>(CartCollectionName);
        return Task.FromResult(collection.FindById(id));
    }

    public Task Update(Cart cart)
    {
        using var db = new LiteDatabase(_connectionString);
        var collection = db.GetCollection<Cart>(CartCollectionName);
        return Task.FromResult(collection.Update(cart));
    }

    public Task<IEnumerable<Cart>> GetAll()
    {
        using var db = new LiteDatabase(_connectionString);
        var collection = db.GetCollection<Cart?>(CartCollectionName);
        return Task.FromResult(collection.FindAll());
    }
}