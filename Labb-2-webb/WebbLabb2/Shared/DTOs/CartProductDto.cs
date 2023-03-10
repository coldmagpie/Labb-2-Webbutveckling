using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbLabb2.Shared.DTOs
{
    public class CartProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsWeightable { get; set; }
    }
}
