using CatalogService.Domain.Models;
using CategoryService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure;

public class ApplicationContext: IApplicationContext
{
    private readonly CatalogDbContext _dbContext;

    public ApplicationContext(CatalogDbContext context)
    {
        _dbContext = context;
        _dbContext.Database.EnsureCreated();
    }

    public Task<Category?> GetCategoryById(long id)
    {
        return _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Category>> GetAllCategories()
    {
        return _dbContext.Categories.ToListAsync();
    }

    public Task AddCategory(Category category)
    {
        _dbContext.Categories.Add(category);
        return _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteCategory(long id)
    {
        var existedCategory = await GetCategoryById(id);
        if (existedCategory is null)
        {
            return false;
        }

        _dbContext.Categories.Remove(existedCategory);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public Task UpdateCategory(Category category)
    {
        _dbContext.Categories.Update(category);
        return _dbContext.SaveChangesAsync();
    }

    public Task<List<Item>> GetItemsByCategoryId(long id, int skip, int limit)
    {
        return _dbContext.Items.Where(x => x.CategoryId == id).Skip(skip).Take(limit).ToListAsync();
    }
}