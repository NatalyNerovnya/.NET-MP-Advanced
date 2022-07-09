using CatalogService.Domain.Models;
using CategoryService.Application.Commands;
using CategoryService.Application.Commands.AddItem;
using CategoryService.Application.Commands.AddOrUpdateCategory;
using CategoryService.Application.Commands.DeleteCategory;
using CategoryService.Application.Commands.DeleteItem;
using CategoryService.Application.Commands.UpdateItem;
using CategoryService.Application.Interfaces.Commands;
using CategoryService.Application.Interfaces.Queries;
using CategoryService.Application.Queries;
using CategoryService.Application.Queries.GetCategory;
using CategoryService.Application.Queries.ListCategories;
using CategoryService.Application.Queries.ListItems;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NotificationClient;

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
        services.AddScoped<IQueryHandler<GetCategoryQuery, Category>, GetCategoryQueryHandler>();

        services.AddScoped<ICommandHandler<AddOrUpdateCategoryCommand>, AddOrUpdateCategoryCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteCategoryCommand>, DeleteCategoryCommandHandler>();
        services.AddScoped<ICommandHandler<AddItemCommand>, AddItemCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateItemCommand>, UpdateItemCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteItemCommand>, DeleteItemCommandHandler>();

        services.AddNotification();
    }
}