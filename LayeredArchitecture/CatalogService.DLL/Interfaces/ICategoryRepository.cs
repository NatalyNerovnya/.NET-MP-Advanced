using CatalogService.Domain.Models;

namespace CatalogService.DLL.Interfaces;

public interface ICategoryRepository
{
    Task<Category> GetById(long id);

    Task<IEnumerable<Category>> GetAll();

    Task<long> Add(Category category);

    Task<bool> Delete(long id);

    Task Update(Category category);
}