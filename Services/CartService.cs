using Microsoft.EntityFrameworkCore;
using ErasmusProject.Models;
using ErasmusProject;
using Microsoft.AspNetCore.Cors.Infrastructure;
namespace ErasmusProject.Services
{
    public class CartService : ICartService
    {
        private readonly Context _context;

        public CartService()
        {
        }

        public CartService(Context context)
        {
            _context = context;
        }

        public async Task<double> CalculateCartTotalAsync(int userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == userId);

            if (cart == null || !cart.CartDetails.Any())
                return 0;

            return cart.CartDetails.Sum(item => item.Product.Price * item.Quantity);
        }

        public async Task<long> CalculateTotalInCentsAsync(int userId)
        {
            double total = await CalculateCartTotalAsync(userId);
            return (long)(total * 100);
        }
    }
}