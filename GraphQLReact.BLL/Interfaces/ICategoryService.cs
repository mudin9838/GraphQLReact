using GraphQLReact.BLL.Dtos;

namespace GraphQLReact.BLL.Interfaces;

public interface ICategoryService
{
    IQueryable<CategoryDto> GetAllCategories(); // Return IQueryable for pagination, sorting, and filtering
    Task<CategoryDto> GetCategoryByIdAsync(int id); // Get a Category by ID
    Task AddCategoryAsync(CategoryDto categoryDto); // Add a new Category
    Task UpdateCategoryAsync(CategoryDto categoryDto); // Update an existing Category
    Task DeleteCategoryAsync(int id); // Delete a Category by ID
}
