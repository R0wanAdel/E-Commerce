using ErasmusProject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ErasmusProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly Context _context;

        public CartController(Context context)
        {
            _context = context;
        }

        private int GetCustomerId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(AddToCartDTO request)
        {
            var customerId = GetCustomerId();

            var cart = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    CreatedAt = DateTime.UtcNow,
                    CustomerId = customerId,
                    CartDetails = new List<CartDetail>()
                };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == request.ProductId);
            if (existingDetail != null)
            {
                existingDetail.Quantity += request.Quantity;
            }
            else
            {
                cart.CartDetails.Add(new CartDetail
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                });
            }

            await _context.SaveChangesAsync();
            return Ok("Producto agregado al carrito.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetCart()
        {
            var customerId = GetCustomerId();

            var cart = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                return Ok(new List<CartItemDTO>());
            }

            var items = cart.CartDetails.Select(cd => new CartItemDTO
            {
                ProductId = cd.ProductId,
                ProductName = cd.Product.Name,
                UnitPrice = cd.Product.Price,
                Quantity = cd.Quantity,
                Total = cd.Product.Price * cd.Quantity
            }).ToList();

            return Ok(items);
        }

        [HttpDelete("RemoveItem/{productId}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var customerId = GetCustomerId();

            var cart = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
                return NotFound("Carrito no encontrado.");

            var item = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == productId);
            if (item == null)
                return NotFound("Producto no encontrado en el carrito.");

            cart.CartDetails.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Producto eliminado del carrito.");
        }
    }
}
