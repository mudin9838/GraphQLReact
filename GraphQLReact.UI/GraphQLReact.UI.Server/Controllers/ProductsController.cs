using GraphQLReact.BLL.Dtos;
using GraphQLReact.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLReact.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IUserContextService _userContextService;
    public ProductsController(IProductService productService, IUserContextService userContextService)
    {
        _productService = productService;
        _userContextService = userContextService;
    }

    // This method is protected by the GlobalPolicy
    [Authorize(Policy = "RequireAdminOrUserRole")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = _userContextService.GetUserId();
            var isAdmin = _userContextService.IsAdmin();

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is null or empty.");
            }

            try
            {
                var products = await _productService.GetProductsAsync(userId, isAdmin);
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library in a real application)
                Console.WriteLine($"Error retrieving products: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        return Unauthorized();
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = _userContextService.GetUserId();
            var isAdmin = _userContextService.IsAdmin();

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is null or empty.");
            }

            try
            {
                var productDto = await _productService.GetProductByIdAsync(id, userId, isAdmin);

                return productDto == null ? (ActionResult<ProductDto>)NotFound() : (ActionResult<ProductDto>)Ok(productDto);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library in a real application)
                Console.WriteLine($"Error retrieving product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        return Unauthorized();
    }

    [Authorize(Policy = "RequireAdminOrUserRole")]
    [HttpPost]

    public async Task<ActionResult<ProductDto>> PostProduct(ProductCreateDto productCreateDto)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized();
        }

        var userId = _userContextService.GetUserId();
        var isAdmin = _userContextService.IsAdmin();

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is null or empty.");
        }

        try
        {
            // Call the service method to add the product and get the created ProductDto
            var productDto = await _productService.AddProductAsync(productCreateDto, userId, isAdmin);

            // Return a response with the newly created product
            return CreatedAtAction(nameof(GetProduct), new { id = productDto.Id }, productDto);
        }
        catch (Exception ex)
        {
            // Log the exception (consider using a logging library in a real application)
            Console.WriteLine($"Error adding product: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    [Authorize(Policy = "RequireAdminOrUserRole")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductUpdateDto productUpdateDto)
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = _userContextService.GetUserId();
            var isAdmin = _userContextService.IsAdmin();

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is null or empty.");
            }

            if (id != productUpdateDto.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            try
            {
                if (!isAdmin)
                {
                    var existingProduct = await _productService.GetProductByIdAsync(id, userId, false);
                    if (existingProduct == null || existingProduct.CategoryId != productUpdateDto.CategoryId)
                    {
                        return Forbid();
                    }
                }

                await _productService.UpdateProductAsync(productUpdateDto, userId, isAdmin);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found.");
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library in a real application)
                Console.WriteLine($"Error updating product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        return Unauthorized();
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (User.Identity.IsAuthenticated)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library in a real application)
                Console.WriteLine($"Error deleting product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        return Unauthorized();
    }
}

