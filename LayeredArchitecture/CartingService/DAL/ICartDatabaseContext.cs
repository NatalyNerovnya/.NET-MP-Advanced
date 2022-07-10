using CartingService.Entities.Models;

namespace CartingService.DAL;

public interface ICartDatabaseContext
{
    Task<Cart?> GetById(int id);

    Task Update(Cart cart);

    Task<IEnumerable<Cart>> GetAll();
}