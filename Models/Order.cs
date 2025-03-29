using System.ComponentModel.DataAnnotations;

namespace ErasmusProject
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
