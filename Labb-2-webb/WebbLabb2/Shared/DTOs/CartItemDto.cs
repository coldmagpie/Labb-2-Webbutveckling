using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbLabb2.Shared.DTOs
{
    public class CartItemDto
    {
        public int productId { get; set; }
        public int Quantity { get; set; } = 1;
        public double Price { get; set; }
    }
}
