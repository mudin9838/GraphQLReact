using GraphQLReact.DLL.Entities;

namespace GraphQLReact.BLL.Dtos;
public class CategoryDto
{
    public int Id { get; set; }         // Unique identifier for the category
    public string Title { get; set; }    // Name of the category
    // Navigation property for products (if needed)
    public IEnumerable<Product>? Products { get; set; }
}

