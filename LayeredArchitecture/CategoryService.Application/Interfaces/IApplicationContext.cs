using CatalogService.Domain.Models;

namespace CategoryService.Application.Interfaces;

public interface IApplicationContext
{
    Task<Category?> GetCategoryById(long id);

    Task<IEnumerable<Category>> GetAllCategories();

    Task<long> AddCategory(Category category);

    Task<bool> DeleteCategory(long id);

    Task UpdateCategory(Category category);
}