using CatalogService.Domain.Models;
using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.AddItem;

public class AddItemCommand: Item, ICommand
{
}