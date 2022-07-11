namespace CartingService.Entities.Models;

public class Cart
{
    public Cart()
    {
        Items = new List<Item>();
    }

    public Cart(int id, IList<Item> items)
    {
        Id = id;
        Items = items;
    }

    public int Id { get; set; }

    public IList<Item> Items { get; set; }
}