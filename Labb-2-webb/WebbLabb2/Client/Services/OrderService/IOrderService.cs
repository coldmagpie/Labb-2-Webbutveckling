using DataAccess.DataAccess.Models;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderDto> PlaceOrder(int userId, List<CartProductDto> cartProducts);
        Task<List<OrderDto>> GetUserOrders(int userId);
        Task<OrderDto> GetOrderById(int userId, int OrderId);
        Task<List<OrderItemsDto>> GetOrderItems(int userId, int orderId);
    }
}
