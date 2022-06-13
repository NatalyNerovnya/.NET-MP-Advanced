using CatalogService.BLL.Interfaces;
using CatalogService.Domain.Models;

namespace CatalogService.BLL.Services;

public class CategoryService: ICategoryService
{
    public Task<Category> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<long> Add(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Category category)
    {
        throw new NotImplementedException();
    }
}