using CatalogService.Domain.Models;
using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.AddItem;

public class AddItemCommand: ICommand
{
    public long CategoryId { get; set; }

    public Item Item { get; set; }
}