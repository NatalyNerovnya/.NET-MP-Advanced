using CartingService.Entities.Models;

namespace CartingService.DAL;

public interface ICartRepository
{
    Task<IEnumerable<Cart>> GetAllCarts();

    Task<Cart?> GetById(int id);

    Task Update(Cart cart);
}