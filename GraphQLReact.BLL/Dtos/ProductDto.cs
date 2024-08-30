using GraphQLReact.DLL.Entities;

namespace GraphQLReact.BLL.Dtos;
public class ProductDto
{

    public int Id { get; set; }          // Unique identifier for the product
    public string Title { get; set; }    // Title of the product
    public string Description { get; set; } // Description of the product
    public decimal Price { get; set; }   // Price of the product
    public string ImageUrl { get; set; } // URL of the product's image
    public int CategoryId { get; set; }  // Identifier for the associated category

    // Navigation property for category (if needed)
    public Category? Category { get; set; }
}

