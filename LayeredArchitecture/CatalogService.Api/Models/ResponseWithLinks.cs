namespace CatalogService.Api.Models;

public class ResponseWithLinks<T>
{
    public T Body { get; set; }

    public IList<Link> Links { get; set; }
}