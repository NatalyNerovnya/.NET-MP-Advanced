using CatalogService.BLL.Interfaces;
using CatalogService.DLL.Interfaces;
using CatalogService.Domain.Models;
using FluentValidation;

namespace CatalogService.BLL.Services;

public class CategoryService: ICategoryService
{
    private readonly AbstractValidator<Category> _validator;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository, AbstractValidator<Category> validator)
    {
        _categoryRepository = categoryRepository;
        _validator = validator;
    }

    public Task<Category> GetById(long id)
    {
        return _categoryRepository.GetById(id);
    }

    public Task<IEnumerable<Category>> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    public async Task<long> Add(Category category)
    {
        if (!(await _validator.ValidateAsync(category)).IsValid)
        {
            throw new Exception("Category is not valid");
        }

        return await _categoryRepository.Add(category);
    }

    public Task<bool> Delete(long id)
    {
        return _categoryRepository.Delete(id);
    }

    public async Task Update(Category category)
    {
        if (!(await _validator.ValidateAsync(category)).IsValid)
        {
            throw new Exception("Updated category is not valid");
        }

        await _categoryRepository.Update(category);
    }
}