

using GraphQLReact.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLReact.DLL.Configuration;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Primary key configuration
        builder.HasKey(p => p.Id);

        // Auto-increment for Id
        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

        // Title configuration
        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(100);

        // Description configuration
        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(500);

        // Price configuration
        builder.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        // ImageUrl configuration
        builder.Property(p => p.ImageUrl)
               .HasMaxLength(2083); // Maximum length for URLs

        // Relationship configuration
        builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.SetNull) // Handle deletions of categories
               .IsRequired(false); // CategoryId is optional
    }
}
