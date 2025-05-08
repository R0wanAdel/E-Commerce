using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErasmusProject.DTOs;
using Microsoft.EntityFrameworkCore;
using Stripe;
using ErasmusProject.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace ErasmusProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly ICartService _cartService;

        public PaymentController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"); // however you track the logged-in user
            long amountInCents = await _cartService.CalculateTotalInCentsAsync(userId);

            var options = new PaymentIntentCreateOptions
            {
                Amount = amountInCents,
                Currency = "eur",
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }
    }
}