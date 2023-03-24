using DataAccess.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbLabb2.Shared.DTOs;
using WebbLabb2.Shared;

namespace DataAccess.DataAccess.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<ServiceResponse<List<OrderItemsModel>>> GetOrderItems(int userId, int orderId);
        Task<ServiceResponse<List<OrderModel>>> GetOrders(int UserId);
        Task<ServiceResponse<OrderModel>> PlaceOrder(int UserId, List<CartProductDto> cartProducts);
        Task<ServiceResponse<OrderModel>> GetOrderById(int UserId, int orderId);
    }
}
