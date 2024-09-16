using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GraphQLReact.DLL.Entities;

namespace GraphQLReact.DLL.Configuration;
public class UserProductConfiguration : IEntityTypeConfiguration<UserProduct>
{
    public void Configure(EntityTypeBuilder<UserProduct> builder)
    {
        // Configure primary key
        builder.HasKey(up => new { up.UserId, up.ProductId });

        // Configure relationships
        builder
            .HasOne(up => up.User)
            .WithMany() // No need for the navigation property in ApplicationUser
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(up => up.Product)
            .WithMany() // No need for the navigation property in Product
            .HasForeignKey(up => up.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}