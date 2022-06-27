using FluentValidation;

namespace CategoryService.Application.Commands.AddOrUpdateCategory;

public class AddOrUpdateCommandValidator: AbstractValidator<AddOrUpdateCategoryCommand>
{
    public AddOrUpdateCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}