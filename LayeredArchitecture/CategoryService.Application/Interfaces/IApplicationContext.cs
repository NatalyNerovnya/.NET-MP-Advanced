using CatalogService.Domain.Models;

namespace CategoryService.Application.Interfaces;

public interface IApplicationContext
{
    Task<Category> GetCategoryById(long id);

    Task<List<Category>> GetAllCategories();

    Task AddCategory(Category category);

    Task DeleteCategory(long id);

    Task UpdateCategory(Category category);

    Task<List<Item>> GetItemsByCategoryId(long id, int skip, int limit);

    Task<Item> GetItem(long itemId);

    Task AddItem(Item item);

    Task UpdateItem(Item item);

    Task DeleteItem(long id);
}