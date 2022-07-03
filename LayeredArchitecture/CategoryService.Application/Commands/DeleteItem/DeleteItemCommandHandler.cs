using CategoryService.Application.Interfaces;
using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.DeleteItem;

public class DeleteItemCommandHandler: ICommandHandler<DeleteItemCommand>
{
    private readonly IApplicationContext _applicationContext;

    public DeleteItemCommandHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task Handle(DeleteItemCommand command)
    {
        return _applicationContext.DeleteItem(command.Id);
    }
}