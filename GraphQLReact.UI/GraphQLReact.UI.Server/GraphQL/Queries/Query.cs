using GraphQLReact.BLL.Interfaces;
using GraphQLReact.BLL.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace GraphQLReact.API.GraphQL;

public class Query
{
    private readonly IUserContextService _userContextService;

    public Query(IUserContextService userContextService)
    {
        _userContextService = userContextService;
    }

    [Authorize(Policy = "RequireAdminOrUserRole")]
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductDto> GetProducts(
        [Service] IProductService productService)
    {
        var userId = _userContextService.GetUserId();
        var isAdmin = _userContextService.IsAdmin();

        // Use an async method to get products, and convert to IQueryable
        return productService.GetProductsAsync(userId, isAdmin).GetAwaiter().GetResult();
    }

    public async Task<ProductDto> GetProductByIdAsync(
        int id,
        [Service] IProductService productService)
    {
        var userId = _userContextService.GetUserId();
        var isAdmin = _userContextService.IsAdmin();

        return await productService.GetProductByIdAsync(id, userId, isAdmin);
    }

    [Authorize(Policy = "RequireAdminOrUserRole")]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<CategoryDto> GetCategories(
        [Service] ICategoryService categoryService)
    {
        return categoryService.GetAllCategories(); // Return IQueryable directly
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(
        int id,
        [Service] ICategoryService categoryService)
    {
        return await categoryService.GetCategoryByIdAsync(id);
    }
}
