using CatalogService.Domain.Models;
using CategoryService.Application.Exceptions;
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

    public async Task<Category> GetCategoryById(long id)
    {
        var existedCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (existedCategory is null)
        {
            throw new NotExistException($"Category with id {id} doesn't exists");
        }

        return existedCategory;
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

    public async Task DeleteCategory(long id)
    {
        var existedCategory = await GetCategoryById(id);
        _dbContext.Categories.Remove(existedCategory);
        var itemsToRemove = await _dbContext.Items.Where(x => x.CategoryId == id).ToListAsync();
        _dbContext.Items.RemoveRange(itemsToRemove);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCategory(Category category)
    {
        await GetCategoryById(category.Id);

        _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Item>> GetItemsByCategoryId(long id, int skip, int limit)
    {
        var existedCategory = await GetCategoryById(id);

        return await _dbContext.Items.Where(x => x.CategoryId == existedCategory.Id).Skip(skip).Take(limit).ToListAsync();
    }

    public async Task AddItem(long categoryId, Item item)
    {
        var category = await GetCategoryById(categoryId);
        var existedItem = await _dbContext.Items.Where(x => item.Id == x.Id).FirstOrDefaultAsync();
        if (existedItem is not null)
        {
            throw new DuplicateException($"Item with id {item.Id} already exists");
        }

        item.CategoryId = category.Id;
        _dbContext.Items.Add(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteItem(long id)
    {
        var existedItem = await _dbContext.Items.Where(x => id == x.Id).FirstOrDefaultAsync();
        if (existedItem is null)
        {
            throw new NotExistException($"Item with id {id} is not exists");
        }

        _dbContext.Items.Remove(existedItem);
        await _dbContext.SaveChangesAsync();
    }
}