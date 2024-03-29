﻿using CartingService.BLL;
using CartingService.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace CartingService;

public static class Setup
{
    public static void AddCartService(this IServiceCollection services, string filePath)
    {
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartDatabaseContext>(s => new CartDatabaseContext($"Filename={filePath};connection=shared"));
    }
}