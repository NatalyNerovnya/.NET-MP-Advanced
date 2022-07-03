using FluentValidation;

namespace CategoryService.Application.Queries.ListItems;

public class ListItemsQueryValidator: AbstractValidator<ListItemsQuery>
{
    public ListItemsQueryValidator()
    {
        RuleFor(x => x.Limit > 0);
        RuleFor(x => x.Skip >= 0);
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}