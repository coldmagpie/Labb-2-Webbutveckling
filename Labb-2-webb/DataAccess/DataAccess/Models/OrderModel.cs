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
        public List<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>();
        public DateTime DateTime { get; set; }
    }
}
