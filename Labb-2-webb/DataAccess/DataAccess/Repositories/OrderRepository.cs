//using DataAccess.DataAccess.DataContext;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DataAccess.DataAccess.Models;
//using Microsoft.EntityFrameworkCore;
//using WebbLabb2.Shared;

//namespace DataAccess.DataAccess.Repositories
//{
//    public class OrderRepository
//    {
//        public StoreContext _storeContext;

//        public OrderRepository(StoreContext storeContext)
//        {
//            _storeContext = storeContext;
//        }

//        public async Task<ServiceResponse<OrderItemsModel>> GetOrderDetails(int orderId)
//        {
//            var response = new ServiceResponse<OrderItemsModel>();
//            var order = await _storeContext.Orders
//                .Include(o => o.OrderItems)
//                .ThenInclude(oi => oi.ProductId)
//                .Include(o => o.OrderItems)
//                .Where(o => o.UserId == .GetUserId() && o.Id == orderId)
//                .OrderByDescending(o => o.OrderDate)
//                .FirstOrDefaultAsync();

//            if (order == null)
//            {
//                response.Success = false;
//                response.Message = "Order not found.";
//                return response;
//            }

//            var orderDetailsResponse = new OrderDetailsResponse
//            {
//                OrderDate = order.OrderDate,
//                TotalPrice = order.TotalPrice,
//                Products = new List<OrderDetailsProductResponse>()
//            };

//            order.OrderItems.ForEach(item =>
//                orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
//                {
//                    ProductId = item.ProductId,
//                    ImageUrl = item.Product.ImageUrl,
//                    ProductType = item.ProductType.Name,
//                    Quantity = item.Quantity,
//                    Title = item.Product.Title,
//                    TotalPrice = item.TotalPrice
//                }));

//            response.Data = orderDetailsResponse;

//            return response;
//        }

//    }
//}
