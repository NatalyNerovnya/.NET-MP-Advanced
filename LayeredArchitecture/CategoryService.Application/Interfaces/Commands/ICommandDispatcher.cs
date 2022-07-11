namespace CategoryService.Application.Interfaces.Commands;

public interface ICommandDispatcher
{
    Task Send<T>(T command) where T : ICommand;
}