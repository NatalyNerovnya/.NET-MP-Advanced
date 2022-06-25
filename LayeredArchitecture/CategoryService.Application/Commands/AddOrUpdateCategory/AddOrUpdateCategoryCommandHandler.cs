using CategoryService.Application.Interfaces;
using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.AddOrUpdateCategory;

public class AddOrUpdateCategoryCommandHandler : ICommandHandler<AddOrUpdateCategoryCommand>
{
    private readonly IApplicationContext _context;

    public AddOrUpdateCategoryCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(AddOrUpdateCategoryCommand command)
    {
        var existedCategory = await _context.GetCategoryById(command.Id);

        if (existedCategory is null)
        {
            await _context.AddCategory(command);
        }
        else
        {
            await _context.UpdateCategory(command);
        }
    }
}