namespace CategoryService.Application.Interfaces.Commands;

public interface ICommandHandler<in T>
{
    Task Handle(T command);
}