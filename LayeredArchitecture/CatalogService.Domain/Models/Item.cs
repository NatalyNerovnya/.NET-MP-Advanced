namespace CatalogService.Domain.Models;

public class Item
{
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public Category? Category { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public Uri? Image { get; set; }

    public decimal? Price { get; set; }

    public int? Amount { get; set; }
}