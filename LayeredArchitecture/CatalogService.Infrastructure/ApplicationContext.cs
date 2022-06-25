using CatalogService.Domain.Models;
using CategoryService.Application.Interfaces;

namespace CatalogService.Infrastructure;

public class ApplicationContext: IApplicationContext
{
    public Task<Category?> GetCategoryById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAllCategories()
    {
        throw new NotImplementedException();
    }

    public Task<long> AddCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCategory(long id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }
}