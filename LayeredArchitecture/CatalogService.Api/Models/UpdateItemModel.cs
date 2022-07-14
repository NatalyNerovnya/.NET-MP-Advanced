namespace CatalogService.Api.Models;

public class UpdateItemModel
{
    public string? Description { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }
}