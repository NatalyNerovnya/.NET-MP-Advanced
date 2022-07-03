using System.Reflection;
using CatalogService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure;

public class CatalogDbContext: DbContext
{
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Item> Items => Set<Item>();

    public CatalogDbContext(DbContextOptions<CatalogDbContext> dbOptions)
        : base(dbOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Category>()
            .HasMany(e => e.Items)
            .WithOne(x => x.Category);

        modelBuilder.Entity<Category>()
        .HasData(new List<Category>()
        {
            new Category() { Id = 1, Name = "Milk products" },
            new Category() { Id = 2, Name = "Bread" },
            new Category() { Id = 3, Name = "Cakes" },
        });

        modelBuilder.Entity<Item>().HasData(new List<Item>()
        {
            new Item() { Id = 1, Name = "Chocolate milk", Amount = 100, CategoryId = 1, Price = 1.11m },
            new Item() { Id = 2, Name = "Strawberry milk", Amount = 75, CategoryId = 1, Price = 1.12m }
        });
    }

}