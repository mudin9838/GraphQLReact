
using GraphQLReact.DLL.Entities;
using GraphQLReact.DLL.Configuration;
using Microsoft.EntityFrameworkCore; // Ensure this namespace is correct

namespace GraphQLReact.DLL.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext()
    {

    }
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    // Define DbSets for entities
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { optionsBuilder.UseNpgsql(@"Host=localhost;Database=ShoppingCart;Username=postgres;Password=root"); }
    // Configure model relationships and apply configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration()); // Assuming you have this
    }
}
