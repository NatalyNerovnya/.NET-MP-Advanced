using CartingService.DAL;
using CartingService.Entities.Exceptions;
using CartingService.Entities.Models;
using FluentValidation;

namespace CartingService.BLL;

public class CartService: ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly AbstractValidator<Item> _itemValidator;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
        _itemValidator = new ItemValidator();
    }

    public async Task<IEnumerable<Item>> GetAllItems(int cartId)
    {
        var cart = await GetCartById(cartId);

        return cart.Items;
    }

    public async Task AddItem(int cartId, Item item)
    {
        var existedCart = await GetCartById(cartId);
        if (existedCart.Items.Any(x => x.Id == item.Id))
        {
            throw new ItemDuplicateException($"Item id={item.Id} already exists in cart cartId={cartId}");
        }

        if ((await _itemValidator.ValidateAsync(item)).IsValid)
        {
            throw new ItemNotValidException($"Item id={item.Id} is not valid");
        }

        existedCart.Items.Add(item);

        await _cartRepository.Update(existedCart);
    }

    public async Task RemoveItem(int cartId, Item item)
    {
        var existedCart = await GetCartById(cartId);
        if (existedCart.Items.All(x => x.Id != item.Id))
        {
            return;
        }

        existedCart.Items.Remove(item);
        await _cartRepository.Update(existedCart);
    }

    public async Task<Cart> GetCartById(int cartId)
    {
        var cart = await _cartRepository.GetById(cartId);
        if (cart is null)
        {
            throw new CartNotFoundException($"Item with id={cartId} is not found");
        }

        return cart;
    }
}