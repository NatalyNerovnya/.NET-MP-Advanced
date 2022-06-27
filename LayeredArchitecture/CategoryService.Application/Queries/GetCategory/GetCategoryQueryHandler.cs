using CatalogService.Domain.Models;
using CategoryService.Application.Interfaces;
using CategoryService.Application.Interfaces.Queries;

namespace CategoryService.Application.Queries.GetCategory;

public class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, Category?>
{
    private readonly IApplicationContext _context;

    public GetCategoryQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public Task<Category?> Handle(GetCategoryQuery query)
    {
        return _context.GetCategoryById(query.Id);
    }
}