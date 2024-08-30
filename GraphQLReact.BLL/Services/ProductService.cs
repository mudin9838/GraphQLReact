using AutoMapper;
using GraphQLReact.BLL.Interfaces;
using GraphQLReact.DLL.Data;
using GraphQLReact.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using GraphQLReact.BLL.Dtos;

namespace GraphQLReact.BLL;

public class ProductService : IProductService
{
    private readonly ProductDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(ProductDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IQueryable<ProductDto> GetAllProducts()
    {
        var products = _context.Products
            .Include(p => p.Category) // Include category details
            .AsQueryable(); // Return IQueryable for pagination

        return _mapper.ProjectTo<ProductDto>(products); // Project to DTO
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category) // Include category details
            .FirstOrDefaultAsync(p => p.Id == id);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task AddProductAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductDto productDto)
    {
        var existingProduct = await _context.Products.FindAsync(productDto.Id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        _mapper.Map(productDto, existingProduct);
        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
