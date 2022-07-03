using CatalogService.Domain.Models;
using CategoryService.Application.Commands;
using CategoryService.Application.Commands.AddOrUpdateCategory;
using CategoryService.Application.Commands.DeleteCategory;
using CategoryService.Application.Interfaces.Commands;
using CategoryService.Application.Interfaces.Queries;
using CategoryService.Application.Queries;
using CategoryService.Application.Queries.ListCategories;
using CategoryService.Application.Queries.ListItems;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CategoryService.Application.Setup;

public static class ApplicationSetup
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        services.AddScoped<AbstractValidator<ListItemsQuery>, ListItemsQueryValidator>();

        services.AddScoped<IQueryHandler<ListCategoryQuery, List<Category>>, ListCategoryQueryHandler>();
        services.AddScoped<IQueryHandler<ListItemsQuery, List<Item>>, ListItemsQueryHandler>();

        services.AddScoped<ICommandHandler<AddOrUpdateCategoryCommand>, AddOrUpdateCategoryCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteCategoryCommand>, DeleteCategoryCommandHandler>();
    }
}