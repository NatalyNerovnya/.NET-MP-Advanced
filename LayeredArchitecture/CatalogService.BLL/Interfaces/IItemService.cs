using CatalogService.Domain.Models;

namespace CatalogService.BLL.Interfaces;

public interface IItemService
{
    Task<Item> GetById(long id);

    Task<IEnumerable<Item>> GetAll();

    Task<long> Add(Item item);

    Task<bool> Delete(long id);

    Task Update(Item item);
}