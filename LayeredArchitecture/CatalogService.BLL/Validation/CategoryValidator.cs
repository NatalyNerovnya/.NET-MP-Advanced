using CatalogService.Domain.Models;
using FluentValidation;

namespace CatalogService.BLL.Validation;

public class CategoryValidator: AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}