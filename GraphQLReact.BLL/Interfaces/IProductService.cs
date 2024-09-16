using GraphQLReact.BLL.Dtos;

namespace GraphQLReact.BLL.Interfaces;

public interface IProductService
{
    // Retrieves products for admins or for users based on role
    Task<IQueryable<ProductDto>> GetProductsAsync(string userId, bool isAdmin);

    // Retrieves a specific product by its ID
    Task<ProductDto> GetProductByIdAsync(int id, string userId, bool isAdmin);

    // Adds a new product (usually only admins should have access)
    Task<ProductDto> AddProductAsync(ProductCreateDto productCreateDto, string userId, bool isAdmin);

    // Updates an existing product (usually only admins should have access)
    Task UpdateProductAsync(ProductUpdateDto productUpdateDto, string userId, bool isAdmin);

    // Deletes a product (usually only admins should have access)
    Task DeleteProductAsync(int id);
}
