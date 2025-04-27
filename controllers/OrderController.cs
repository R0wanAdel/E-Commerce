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
    public class OrderController : ControllerBase
    {
        private readonly Context _context;

        public OrderController(Context context)
        {
            _context = context;
        }

        private int GetCustomerId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            var customerId = GetCustomerId();

            var cart = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null || !cart.CartDetails.Any())
            {
                return BadRequest("El carrito está vacío.");
            }

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalAmount = cart.CartDetails.Sum(cd => cd.Product.Price * cd.Quantity),
                Payment = new Payment
                {
                    Amount = cart.CartDetails.Sum(cd => cd.Product.Price * cd.Quantity),
                    PaymentDate = DateTime.UtcNow
                },
                OrderDetails = cart.CartDetails.Select(cd => new OrderDetail
                {
                    Quantity = cd.Quantity,
                    Subtotal = cd.Product.Price * cd.Quantity
                }).ToList(),
                OrderProducts = cart.CartDetails.Select(cd => new OrderProduct
                {
                    ProductId = cd.ProductId
                }).ToList()
            };

            _context.Orders.Add(order);

            _context.CartDetails.RemoveRange(cart.CartDetails);
            await _context.SaveChangesAsync();

            return Ok("Orden creada exitosamente.");
        }
    }
}
