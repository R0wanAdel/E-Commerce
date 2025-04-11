using ErasmusProject.DTOs;
using ErasmusProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ErasmusProject.Controllers
{
    public class AccountController:ControllerBase
    {
        private readonly Context _context;

        public AccountController(Context context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            // Validate the incoming model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check if the email already exists
            if (await _context.Customers.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Email is already in use.");
            // Create password hash and salt
            PasswordHasher.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
            // Create user entity
            var user = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            // Add user to the database
            _context.Customers.Add(user);
            await _context.SaveChangesAsync();
            // For security reasons, do not return password hash and salt
            return Ok(new { Message = "User registered successfully." });
        }
    }
}
