using CatalogService.Domain.Models;
using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.AddOrUpdateCategory;

public class AddOrUpdateCategoryCommand : Category, ICommand
{
}