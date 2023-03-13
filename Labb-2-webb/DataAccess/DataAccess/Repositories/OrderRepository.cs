using DataAccess.DataAccess.DataContext;
using DataAccess.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;
using DataAccess.DataAccess.Interfaces;

namespace DataAccess.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public StoreContext _storeContext;

        public OrderRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<ServiceResponse<List<OrderItemsModel>>> GetOrderItems(int userId, int orderId)
        {
            var response = new ServiceResponse<List<OrderItemsModel>>()
            {
                Data = new List<OrderItemsModel>()
            };
            var order = await _storeContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId && o.Id == orderId)
                .OrderByDescending(o => o.DateTime)
                .FirstOrDefaultAsync();

            if (order is null)
            {
                response.Error = true;
                response.Message = "Order not found.";
            }
            else
            {
                foreach (var item in order.OrderItems)
                {
                    response.Data.Add(item);
                }
            }
            return response;
        }
        public async Task<ServiceResponse<OrderModel>> PlaceOrder(int UserId, List<CartProductDto> cartProducts)
        {
            var response = new ServiceResponse<OrderModel>();
            var user = await _storeContext.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            var order = new OrderModel()
            {
                DateTime = DateTime.Now,
                UserId = user.Id,
                OrderItems = new List<OrderItemsModel>()
            };
            await _storeContext.Orders.AddAsync(order);
            await _storeContext.SaveChangesAsync();

            foreach (var item in cartProducts)
            {
                var product = await _storeContext.Products
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var orderItem = new OrderItemsModel()
                {
                    OrderId = order.Id,
                    Product = product,
                    PriceEach = product.Price,
                    Quantity = item.Quantity,
                };
                order.OrderItems.Add(orderItem);
            }

            
            await _storeContext.SaveChangesAsync();
            response.Data = order;
            return response;
        }

        public async Task<ServiceResponse<OrderModel>> GetOrderById(int UserId, int orderId)
        {
            var response = new ServiceResponse<OrderModel>();
            var user = await _storeContext.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (user == null)
            {
                response.Error = true;
                response.Message = "User does't exist!";
            }

            var order = await _storeContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .Where(u => u.UserId == user.Id)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            response.Data = order;
            return response;
        }

        public async Task<ServiceResponse<List<OrderModel>>> GetOrders(int UserId)
        {
            var response = new ServiceResponse<List<OrderModel>>();
            var user = await _storeContext.Users.FirstOrDefaultAsync(u => u.Id == UserId);

            if (user == null)
            {
                response.Error = true;
                response.Message = "User does't exist!";
            }
            var orders = await _storeContext.Orders
                .Include(o=>o.OrderItems)
                .ThenInclude(o=>o.Product)
                .Where(u=>u.UserId == user.Id)
                .ToListAsync();
            response.Data = orders;
            return response;
        }

    }
}
