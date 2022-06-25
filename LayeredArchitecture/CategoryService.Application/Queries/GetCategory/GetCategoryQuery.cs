using CategoryService.Application.Interfaces.Queries;

namespace CategoryService.Application.Queries.GetCategory;

public class GetCategoryQuery : IQuery
{
    public int Id { get; set; }
}