using System.Reflection;
using CatalogService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure;

public class CatalogDbContext: DbContext
{
    public DbSet<Category> Categories => Set<Category>();

    public CatalogDbContext(DbContextOptions<CatalogDbContext> dbOptions)
        : base(dbOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Category>().HasData(new List<Category>()
        {
            new Category() { Id = 1, Name = "Milk products" },
            new Category() { Id = 2, Name = "Bread" },
            new Category() { Id = 3, Name = "Cakes" },
        });
    }

}