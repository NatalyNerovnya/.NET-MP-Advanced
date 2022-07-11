using CatalogService.Domain.Models;
using CategoryService.Application.Interfaces;
using CategoryService.Application.Interfaces.Queries;
using FluentValidation;

namespace CategoryService.Application.Queries.ListItems;

public class ListItemsQueryHandler: IQueryHandler<ListItemsQuery, List<Item>>
{
    private readonly IApplicationContext _context;
    private readonly AbstractValidator<ListItemsQuery> _validator;

    public ListItemsQueryHandler(IApplicationContext context, AbstractValidator<ListItemsQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<List<Item>> Handle(ListItemsQuery query)
    {
        var validationResult = await _validator.ValidateAsync(query);
        if (!validationResult.IsValid)
        {
            throw new Exception($"Not valid request: {string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage))}");
        }

        return await _context.GetItemsByCategoryId(query.CategoryId, query.Skip, query.Limit);
    }
}