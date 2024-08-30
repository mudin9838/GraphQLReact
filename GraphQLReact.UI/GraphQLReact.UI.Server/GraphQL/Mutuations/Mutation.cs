using GraphQLReact.BLL.Interfaces;
using GraphQLReact.BLL.Dtos;

namespace GraphQLReact.API.GraphQL;

public class Mutation
{
    public async Task<ProductDto> CreateProductAsync(
        ProductDto productDto,
        [Service] IProductService productService)
    {
        await productService.AddProductAsync(productDto);
        return productDto;
    }

    public async Task<ProductDto> UpdateProductAsync(
        ProductDto productDto,
        [Service] IProductService productService)
    {
        await productService.UpdateProductAsync(productDto);
        return productDto;
    }

    public async Task<bool> DeleteProductAsync(
        int id,
        [Service] IProductService productService)
    {
        await productService.DeleteProductAsync(id);
        return true;
    }

    // Category mutations
    public async Task<CategoryDto> CreateCategoryAsync(
        CategoryDto categoryDto,
        [Service] ICategoryService categoryService)
    {
        await categoryService.AddCategoryAsync(categoryDto);
        return categoryDto;
    }

    public async Task<CategoryDto> UpdateCategoryAsync(
        CategoryDto categoryDto,
        [Service] ICategoryService categoryService)
    {
        await categoryService.UpdateCategoryAsync(categoryDto);
        return categoryDto;
    }

    public async Task<bool> DeleteCategoryAsync(
        int id,
        [Service] ICategoryService categoryService)
    {
        await categoryService.DeleteCategoryAsync(id);
        return true;
    }
}

