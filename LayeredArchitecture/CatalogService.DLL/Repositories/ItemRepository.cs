using CatalogService.DLL.Interfaces;
using CatalogService.Domain.Models;

namespace CatalogService.DLL.Repositories;

public class ItemRepository: IItemRepository
{
    public Task<Item> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Item>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<long> Add(Item item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Item item)
    {
        throw new NotImplementedException();
    }
}