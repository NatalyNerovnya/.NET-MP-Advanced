using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.DeleteItem;

public class DeleteItemCommand: ICommand
{
    public long Id { get; set; }
}