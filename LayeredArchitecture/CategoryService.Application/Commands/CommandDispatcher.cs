using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands;

public class CommandDispatcher: ICommandDispatcher
{
    private readonly IServiceProvider _service;

    public CommandDispatcher(IServiceProvider service)
    {
        _service = service;
    }

    public Task Send<T>(T command) where T : ICommand
    {
        var handler = _service.GetService(typeof(ICommandHandler<T>));
        if (handler != null)
        {
            return ((ICommandHandler<T>)handler).Handle(command);
        }
        else
        {
            throw new Exception($"Command doesn't have any handler {command.GetType().Name}");
        }
    }
}