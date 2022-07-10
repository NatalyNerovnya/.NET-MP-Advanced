using CartingService.Entities.Models;

namespace CartingService.DAL;

public class CartRepository : ICartRepository
{
    private readonly ICartDatabaseContext _cartDatabaseContext;

    public CartRepository(ICartDatabaseContext cartDatabaseContext)
    {
        _cartDatabaseContext = cartDatabaseContext;
    }

    public Task<IEnumerable<Cart>> GetAllCarts()
    {
        return _cartDatabaseContext.GetAll();
    }

    public Task<Cart?> GetById(int id)
    {
        return _cartDatabaseContext.GetById(id);
    }

    public Task Update(Cart cart)
    {
        return _cartDatabaseContext.Update(cart);
    }
}