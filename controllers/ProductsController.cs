using Microsoft.AspNetCore.Mvc;
using ErasmusProject.Models;
using ErasmusProject.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ErasmusProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly Context _context;

        public ProductsController(Context context)
        {
            _context = context;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                AdminId = product.AdminId
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Product added successfully.",
                productId = newProduct.ProductId
            });
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products
                .Select(p => new ProductResponseDTO
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = (double)p.Price,
                    Stock = p.Stock
                })
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/Products/{id}
        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            var dto = new ProductResponseDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Stock = product.Stock
            };

            return Ok(dto);
        }*/
        //get product by name 
        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x=>x.Name.ToLower() == name.ToLower());
            if (product == null)
                return NotFound();

            var dto = new ProductResponseDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Stock = product.Stock
            };

            return Ok(dto);
        }

        // PUT: api/Products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO updateDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = updateDto.Name;
            product.Description = updateDto.Description;
            product.Price = updateDto.Price;
            product.Stock = updateDto.Stock;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Product updated successfully." });
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully." });
        }
    }


    public class ProductResponseDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
