using CatalogService.Domain.Models;

namespace CatalogService.DLL.Interfaces;

public interface IItemRepository
{
    Task<Item> GetById(long id);

    Task<IEnumerable<Item>> GetAll();

    Task<long> Add(Item item);

    Task<bool> Delete(long id);

    Task Update(Item item);
}