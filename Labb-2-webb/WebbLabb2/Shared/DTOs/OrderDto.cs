using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbLabb2.Shared.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<OrderItemsDto> OrderItems { get; set; } = new List<OrderItemsDto>();
        public DateTime DateTime { get; set; }
    }
}
