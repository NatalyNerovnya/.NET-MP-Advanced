using CategoryService.Application.Interfaces;
using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.AddItem;

public class AddItemCommandHandler: ICommandHandler<AddItemCommand>
{
    private readonly IApplicationContext _applicationContext;

    public AddItemCommandHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task Handle(AddItemCommand command)
    {
        return _applicationContext.AddItem(command.CategoryId, command.Item);
    }
}