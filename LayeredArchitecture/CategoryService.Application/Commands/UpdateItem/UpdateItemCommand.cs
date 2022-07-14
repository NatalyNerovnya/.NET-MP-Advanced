using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.UpdateItem;

public class UpdateItemCommand : ICommand
{
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? Amount { get; set; }
}