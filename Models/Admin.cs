using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ErasmusProject.Models;

namespace ErasmusProject
{
    public class Admin
    {
        [Key]
        [Required]
        public int AdminId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
