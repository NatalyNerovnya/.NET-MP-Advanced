using CatalogService.BLL.Interfaces;
using CatalogService.DLL.Interfaces;
using CatalogService.Domain.Models;
using FluentValidation;

namespace CatalogService.BLL.Services;

public class ItemService : IItemService
{
    private readonly AbstractValidator<Item> _validator;
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository, AbstractValidator<Item> validator)
    {
        _itemRepository = itemRepository;
        _validator = validator;
    }

    public Task<Item> GetById(long id)
    {
        return _itemRepository.GetById(id);
    }

    public Task<IEnumerable<Item>> GetAll()
    {
        return _itemRepository.GetAll();
    }

    public async Task<long> Add(Item item)
    {
        if (!(await _validator.ValidateAsync(item)).IsValid)
        {
            throw new Exception("Item is not valid");
        }

        return await _itemRepository.Add(item);
    }

    public Task<bool> Delete(long id)
    {
        return _itemRepository.Delete(id);
    }

    public async Task Update(Item item)
    {
        if (!(await _validator.ValidateAsync(item)).IsValid)
        {
            throw new Exception("Updated item is not valid");
        }

        await _itemRepository.Update(item);
    }
}