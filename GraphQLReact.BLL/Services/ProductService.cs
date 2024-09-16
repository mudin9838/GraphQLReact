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

    public async Task<IQueryable<ProductDto>> GetProductsAsync(string userId, bool isAdmin)
    {
        IQueryable<Product> productsQuery;

        if (isAdmin)
        {
            // Admin can view all products
            productsQuery = _context.Products
                .Include(p => p.Category); // Include related category data
        }
        else
        {
            // Regular users can only view their own products
            productsQuery = _context.UserProducts
                .Where(up => up.UserId == userId)
                .Include(up => up.Product) // Include the related Product data
                .ThenInclude(p => p.Category) // Include the related Category data
                .Select(up => up.Product);
        }

        // Convert to IQueryable<ProductDto> using AutoMapper
        var productsDtoQuery = _mapper.ProjectTo<ProductDto>(productsQuery);

        return productsDtoQuery;
    }

    public async Task<ProductDto> GetProductByIdAsync(int id, string userId, bool isAdmin)
    {
        Product product;

        if (isAdmin)
        {
            // Admin can view any product
            product = await _context.Products
                .Include(p => p.Category) // Include related Category data
                .FirstOrDefaultAsync(p => p.Id == id);

            // Log product retrieval for debugging
            if (product == null)
            {
                Console.WriteLine($"Admin attempted to access non-existing product with ID: {id}");
            }
        }
        else
        {
            // Regular users can only view their own products
            var userProduct = await _context.UserProducts
                .Where(up => up.UserId == userId && up.ProductId == id)
                .Include(up => up.Product) // Include the product related to the user
                .ThenInclude(up => up.Category)
                .FirstOrDefaultAsync();

            product = userProduct?.Product; // Regular user can view only their own product

            // Log user product retrieval for debugging
            if (product == null)
            {
                Console.WriteLine($"User {userId} attempted to access product ID {id} they do not own.");
            }
        }

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> AddProductAsync(ProductCreateDto productCreateDto, string userId, bool isAdmin)
    {
        // Map ProductCreateDto to Product
        var product = _mapper.Map<Product>(productCreateDto);

        if (isAdmin)
        {
            // Admin can add products without user association
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        else
        {
            // If the user is not an admin, add the product and associate it with the user
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var userProduct = new UserProduct
            {
                UserId = userId,
                ProductId = product.Id
            };

            _context.UserProducts.Add(userProduct);
            await _context.SaveChangesAsync();
        }

        // Return the newly created ProductDto
        return _mapper.Map<ProductDto>(product);
    }


    public async Task UpdateProductAsync(ProductUpdateDto productUpdateDto, string userId, bool isAdmin)
    {
        var existingProduct = await _context.Products.FindAsync(productUpdateDto.Id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        // Only update if the user is an admin or the product belongs to the user
        if (isAdmin || (await _context.UserProducts.AnyAsync(up => up.UserId == userId && up.ProductId == productUpdateDto.Id)))
        {
            _mapper.Map(productUpdateDto, existingProduct);
            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new UnauthorizedAccessException("You do not have permission to update this product.");
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        // Only admins should be allowed to delete products
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
