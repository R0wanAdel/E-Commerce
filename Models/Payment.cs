using System.ComponentModel.DataAnnotations;

namespace ErasmusProject
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}
