using GraphQLReact.BLL.Interfaces;
using GraphQLReact.BLL.Dtos;

namespace GraphQLReact.API.GraphQL;

public class Mutation
{
    private readonly IUserContextService _userContextService;

    public Mutation(IUserContextService userContextService)
    {
        _userContextService = userContextService;
    }

    public async Task<ProductDto> CreateProductAsync(
        ProductCreateDto productCreateDto,
        [Service] IProductService productService,
        [Service] IHttpContextAccessor httpContextAccessor)
    {
        var userId = _userContextService.GetUserId();
        var isAdmin = _userContextService.IsAdmin();

        if (string.IsNullOrEmpty(userId))
        {
            throw new GraphQLException("User ID is null or empty.");
        }

        try
        {
            var productDto = await productService.AddProductAsync(productCreateDto, userId, isAdmin);
            return productDto;
        }
        catch (Exception ex)
        {
            // Consider using a logging library for real applications
            Console.WriteLine($"Error adding product: {ex.Message}");
            throw new GraphQLException("Internal server error");
        }
    }

    public async Task<ProductDto> UpdateProductAsync(
        ProductUpdateDto productUpdateDto,
        [Service] IProductService productService)
    {
        var userId = _userContextService.GetUserId();
        var isAdmin = _userContextService.IsAdmin();

        if (string.IsNullOrEmpty(userId))
        {
            throw new GraphQLException("User ID is null or empty.");
        }

        try
        {
            await productService.UpdateProductAsync(productUpdateDto, userId, isAdmin);
            return await productService.GetProductByIdAsync(productUpdateDto.Id, userId, isAdmin);
        }
        catch (Exception ex)
        {
            // Consider using a logging library for real applications
            Console.WriteLine($"Error updating product: {ex.Message}");
            throw new GraphQLException("Internal server error");
        }
    }

    public async Task<bool> DeleteProductAsync(
        int id,
        [Service] IProductService productService)
    {
        var isAdmin = _userContextService.IsAdmin();

        if (!isAdmin)
        {
            throw new GraphQLException("Access denied. User does not have Admin role.");
        }

        try
        {
            await productService.DeleteProductAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            // Consider using a logging library for real applications
            Console.WriteLine($"Error deleting product: {ex.Message}");
            throw new GraphQLException("Internal server error");
        }
    }

    // Category mutations
    public async Task<CategoryDto> CreateCategoryAsync(
        CategoryDto categoryDto,
        [Service] ICategoryService categoryService)
    {
        try
        {
            await categoryService.AddCategoryAsync(categoryDto);
            return await categoryService.GetCategoryByIdAsync(categoryDto.Id);
        }
        catch (Exception ex)
        {
            // Consider using a logging library for real applications
            Console.WriteLine($"Error creating category: {ex.Message}");
            throw new GraphQLException("Internal server error");
        }
    }

    public async Task<CategoryDto> UpdateCategoryAsync(
        CategoryDto categoryDto,
        [Service] ICategoryService categoryService)
    {
        try
        {
            await categoryService.UpdateCategoryAsync(categoryDto);
            return await categoryService.GetCategoryByIdAsync(categoryDto.Id);
        }
        catch (Exception ex)
        {
            // Consider using a logging library for real applications
            Console.WriteLine($"Error updating category: {ex.Message}");
            throw new GraphQLException("Internal server error");
        }
    }

    public async Task<bool> DeleteCategoryAsync(
        int id,
        [Service] ICategoryService categoryService)
    {
        try
        {
            await categoryService.DeleteCategoryAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            // Consider using a logging library for real applications
            Console.WriteLine($"Error deleting category: {ex.Message}");
            throw new GraphQLException("Internal server error");
        }
    }
}
