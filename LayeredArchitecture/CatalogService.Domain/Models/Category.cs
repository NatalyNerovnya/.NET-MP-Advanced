namespace CatalogService.Domain.Models;

public class Category
{
    public long Id { get; set; }

    public string Name { get; set; }

    public Uri? Image { get; set; }
}