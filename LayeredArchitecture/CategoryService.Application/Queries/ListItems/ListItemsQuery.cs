namespace CategoryService.Application.Queries.ListItems;

public class ListItemsQuery
{
    public long CategoryId { get; set; }

    public int Skip { get; set; }

    public int Limit { get; set; }
}