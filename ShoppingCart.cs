using System.ComponentModel.DataAnnotations;

namespace ErasmusProject
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
