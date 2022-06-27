using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.DeleteCategory;

public class DeleteCategoryCommand : ICommand
{
    public int Id { get; set; }
}