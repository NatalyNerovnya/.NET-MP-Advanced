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
        return _dbContext.Categories.Include(x => x.Items).ToListAsync();
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
        var itemsToRemove = await _dbContext.Items.Where(x => x.CategoryId == id).ToListAsync();
        _dbContext.Items.RemoveRange(itemsToRemove);
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

    public async Task AddItem(long categoryId, Item item)
    {
        var category = await GetCategoryById(categoryId);
        if (category is null)
        {
            throw new Exception($"Category with id {categoryId} doesn't exists");
        }

        var existedItem = await _dbContext.Items.Where(x => item.Id == x.Id).FirstOrDefaultAsync();
        if (existedItem is not null)
        {
            throw new Exception($"Item with id {item.Id} already exists");
        }

        item.CategoryId = categoryId;
        _dbContext.Items.Add(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteItem(long id)
    {
        var existedItem = await _dbContext.Items.Where(x => id == x.Id).FirstOrDefaultAsync();
        if (existedItem is null)
        {
            throw new Exception($"Item with id {id} is not exists");
        }

        _dbContext.Items.Remove(existedItem);
        await _dbContext.SaveChangesAsync();
    }
}