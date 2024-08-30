using AutoMapper;
using GraphQLReact.BLL.Interfaces;
using GraphQLReact.DLL.Data;
using GraphQLReact.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using GraphQLReact.BLL.Dtos;

namespace GraphQLReact.BLL;

public class CategoryService : ICategoryService
{
    private readonly ProductDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(ProductDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IQueryable<CategoryDto> GetAllCategories()
    {
        var categories = _context.Categories
            .Include(p => p.Products) // Include category details
            .AsQueryable(); // Return IQueryable for pagination

        return _mapper.ProjectTo<CategoryDto>(categories); // Project to DTO
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
            .Include(p => p.Products)
            .FirstOrDefaultAsync(p => p.Id == id);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task AddCategoryAsync(CategoryDto CategoryDto)
    {
        var category = _mapper.Map<Category>(CategoryDto);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(CategoryDto CategoryDto)
    {
        var existingCategory = await _context.Categories.FindAsync(CategoryDto.Id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException("Category not found.");
        }

        _mapper.Map(CategoryDto, existingCategory);
        _context.Categories.Update(existingCategory);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException("Category not found.");
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}
