namespace CategoryService.Application.Interfaces.Commands;

public interface ICommandsDispatcher
{
    void Send<T>(T command) where T : ICommand;
}