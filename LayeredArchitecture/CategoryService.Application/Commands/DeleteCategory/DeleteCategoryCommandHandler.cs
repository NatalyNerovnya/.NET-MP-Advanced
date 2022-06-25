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

    public Task Handle(DeleteCategoryCommand command)
    {
        return _context.DeleteCategory(command.Id);
    }
}