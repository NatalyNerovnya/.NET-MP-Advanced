using CategoryService.Application.Exceptions;
using CategoryService.Application.Interfaces;
using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    private readonly IApplicationContext _context;

    public DeleteCategoryCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryCommand command)
    {
        var existedCategory = await _context.GetCategoryById(command.Id);
        if (existedCategory is null)
        {
            throw new NotExistException($"Category {command.Id} doesn't exists");
        }

        await _context.DeleteCategory(command.Id);
    }
}