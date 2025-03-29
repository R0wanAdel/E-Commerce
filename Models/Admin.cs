using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ErasmusProject
{
    public class Admin
    {
        [Key] 
        [Required]
        public int AdminId { get; set; }
        [Required]
        public  string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Payment> Payments { get; set; } 
    }
}
