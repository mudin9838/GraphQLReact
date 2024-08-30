using GraphQLReact.BLL.Interfaces;
using GraphQLReact.BLL.Dtos;

namespace GraphQLReact.API.GraphQL;

public class Query
{
    // [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductDto> GetProducts(
        [Service] IProductService productService)
    {
        return productService.GetAllProducts(); // Return IQueryable
    }

    public async Task<ProductDto> GetProductByIdAsync(
        int id,
        [Service] IProductService productService)
    {
        return await productService.GetProductByIdAsync(id);
    }

    // Category queries
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<CategoryDto> GetCategories(
        [Service] ICategoryService categoryService)
    {
        return categoryService.GetAllCategories(); // Return IQueryable
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(
        int id,
        [Service] ICategoryService categoryService)
    {
        return await categoryService.GetCategoryByIdAsync(id);
    }
}
