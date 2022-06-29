using CatalogService.Domain.Models;
using CategoryService.Application.Interfaces;
using CategoryService.Application.Interfaces.Queries;

namespace CategoryService.Application.Queries.ListCategories;

public class ListCategoryQueryHandler: IQueryHandler<ListCategoryQuery, List<Category>>
{
    private readonly IApplicationContext _context;

    public ListCategoryQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public Task<List<Category>> Handle(ListCategoryQuery query)
    {
        return _context.GetAllCategories();
    }
}