using CartingService.Entities.Models;

namespace CartingService.DAL;

public interface ICartRepository
{
    Task<List<Cart?>> GetAllCarts();

    Task<Cart?> GetById(int id);

    Task Update(Cart cart);
}