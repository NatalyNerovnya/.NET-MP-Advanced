using CatalogService.Domain.Models;
using FluentValidation;

namespace CatalogService.BLL.Validation;

public class ItemValidator: AbstractValidator<Item>
{
    public ItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}