using DataAccess.DataAccess.Models;
using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public string Message { get; set; } = "We work hard to get the products....";
        public List<OrderDto> Orders { get; set; } = new();
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDto> PlaceOrder(int userId, List<CartProductDto> cartProducts)
        {
            var result = await _httpClient.PostAsJsonAsync($"/user/placeorder/{userId}", cartProducts);
            var order = new OrderDto()
            {
                DateTime = DateTime.Now,
                UserId = userId,
                OrderItems = new List<OrderItemsDto>()
            };

            foreach (var item in cartProducts)
            {
                var product = await _httpClient.GetFromJsonAsync<ProductDto>($"/products/{item.ProductId}");
                if (product == null)
                {
                    continue;
                }

                var orderItem = new OrderItemsDto()
                {
                    OrderId = order.Id,
                    Product = product,
                    PriceEach = product.Price,
                    Quantity = item.Quantity,
                };
                order.OrderItems.Add(orderItem);
            }
            return order;
        }

        public async Task<List<OrderDto>> GetUserOrders(int userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<OrderDto>>($"/user/{userId}/orders");
            Orders = result.ToList();
            return Orders;
        }

        public async Task<OrderDto> GetOrderById(int userId, int orderId)
        {
            var result = await _httpClient.GetFromJsonAsync<OrderDto>($"/user/{userId}/order/{orderId}");
            return result;
        }

        public async Task<List<OrderItemsDto>> GetOrderItems(int userId, int orderId)
        {
            var result =
                await _httpClient.GetFromJsonAsync<List<OrderItemsDto>>($"/user/{userId}/order/{orderId}/detail");
            var orderItems = result.ToList();
            return orderItems;
        }
    }
}
