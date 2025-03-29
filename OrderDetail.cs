using System.ComponentModel.DataAnnotations;

namespace ErasmusProject
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
