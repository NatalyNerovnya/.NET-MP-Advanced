using CartingService.Entities.Models;

namespace CartingService.DAL;

public interface ICartRepository
{
    Task<Cart?> GetById(int id);

    Task Update(Cart cart);
}