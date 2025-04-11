using System.ComponentModel.DataAnnotations;

namespace ErasmusProject
{
    public class Customer
    {
        [Key]
        [Required]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public  string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
