using CartingService.Entities.Models;

namespace CartingService.BLL;

public interface ICartService
{
    Task<IEnumerable<Item>> GetAllItems(int cartId);

    Task<Cart> GetCartById(int cartId);

    Task AddItem(int cartId, Item item);

    Task RemoveItem(int cartId, int item);
}