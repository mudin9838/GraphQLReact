using GraphQLReact.BLL.Dtos;

namespace GraphQLReact.BLL.Interfaces;

public interface IProductService
{
    IQueryable<ProductDto> GetAllProducts(); // Return IQueryable for pagination, sorting, and filtering
    Task<ProductDto> GetProductByIdAsync(int id); // Get a product by ID
    Task AddProductAsync(ProductDto productDto); // Add a new product
    Task UpdateProductAsync(ProductDto productDto); // Update an existing product
    Task DeleteProductAsync(int id); // Delete a product by ID
}
