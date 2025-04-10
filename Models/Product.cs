using System.ComponentModel.DataAnnotations;

namespace ErasmusProject
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
