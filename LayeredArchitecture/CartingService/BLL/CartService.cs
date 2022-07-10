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

    public async Task EditItem(Item item)
    {
        var carts = await _cartRepository.GetAllCarts();
        var tasks = new List<Task>();
        foreach (var cart in carts.Where(x => x.Items.Any(i => i.Id == item.Id)))
        {
            var oldItem = cart.Items.First(x => x.Id == item.Id);
            cart.Items.Remove(oldItem);
            cart.Items.Add(item);
            tasks.Add(_cartRepository.Update(cart));
        }

        await Task.WhenAll(tasks);
    }

    public async Task AddItem(int cartId, Item item)
    {
        var existedCart = await GetCartById(cartId);
        if (existedCart.Items.Any(x => x.Id == item.Id))
        {
            throw new ItemDuplicateException($"Item id={item.Id} already exists in cart cartId={cartId}");
        }

        if (!(await _itemValidator.ValidateAsync(item)).IsValid)
        {
            throw new ItemNotValidException($"Item id={item.Id} is not valid");
        }

        existedCart.Items.Add(item);

        await _cartRepository.Update(existedCart);
    }

    public async Task RemoveItem(int cartId, int itemId)
    {
        var existedCart = await GetCartById(cartId);
        if (existedCart.Items.All(x => x.Id != itemId))
        {
            throw new ItemNotFoundException($"Item with id={itemId} is not found");
        }

        var existedItem = existedCart.Items.First(x => x.Id == itemId);
        existedCart.Items.Remove(existedItem);
        await _cartRepository.Update(existedCart);
    }

    public async Task<Cart> GetCartById(int cartId)
    {
        var cart = await _cartRepository.GetById(cartId);
        if (cart is null)
        {
            throw new CartNotFoundException($"Cart with id={cartId} is not found");
        }

        return cart;
    }
}