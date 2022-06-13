using CatalogService.Domain.Models;

namespace CatalogService.BLL.Interfaces;

public interface ICategoryService
{
    Task<Category> GetById(long id);
    Task<IEnumerable<Category>> GetAll();
    Task<long> Add(Category category);
    Task<bool> Delete(long id);
    Task Update(Category category);

}