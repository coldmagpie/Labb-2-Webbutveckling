using DataAccess.DataAccess.Interfaces;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions
{
    public static class OrderExtension
    {
        public static WebApplication MapOrderEndpoints(this WebApplication app)
        {
            app.MapPost("/user/placeorder/{userId}", PlaceOrderAsync);
            app.MapGet("/user/{userId}/orders", GetOrdersAsync);
            app.MapGet("/user/{userId}/order/{orderId}/detail", GetOrderItems);
            app.MapGet("/user/{userId}/order/{orderId}", GetOrderById);

            return app;
        }

        private static async Task<IResult> GetOrderById(IOrderRepository repo, int userId, int orderId)
        {
            var response = await repo.GetOrderById(userId, orderId);
            return response.Error ? Results.BadRequest("Sorry, place order failed") : Results.Ok(response);
        }

        private static async Task<IResult> PlaceOrderAsync(IOrderRepository repo, int userId, List<CartProductDto> cartProducts)
        {
            var response = await repo.PlaceOrder(userId, cartProducts);
            return response.Error ? Results.BadRequest("Sorry, place order failed") : Results.Ok(response);
        }
        private static async Task<IResult> GetOrdersAsync(IOrderRepository repo, int userId)
        {
            var response = await repo.GetOrders(userId);
            return response.Error ? Results.BadRequest("You have no order") : Results.Ok(response);
        }
        private static async Task<IResult> GetOrderItems(IOrderRepository repo, int userId, int orderId)
        {
            var response = await repo.GetOrderItems(userId, orderId);
            return response.Error ? Results.BadRequest("Sorry, order not found") : Results.Ok(response);
        }

    }
}
