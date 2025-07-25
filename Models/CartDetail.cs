﻿using System.ComponentModel.DataAnnotations;

namespace ErasmusProject
{
    public class CartDetail
    {
        [Key]
        public int DetailId { get; set; }

        public int Quantity { get; set; }

        public double Subtotal { get; set; }

        public int CartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
