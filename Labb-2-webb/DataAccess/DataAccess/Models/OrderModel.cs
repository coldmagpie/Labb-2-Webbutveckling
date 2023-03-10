using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccess.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<OrderItemsModel> OrderItems { get; set; } = new List<OrderItemsModel>();
        public DateTime DateTime { get; set; }
    }
}
