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
            var serviceResponse = await repo.GetOrderById(userId, orderId);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Sorry, place order failed");
            }
            return Results.Ok(serviceResponse.Data);
        }

        private static async Task<IResult> PlaceOrderAsync(IOrderRepository repo, int userId, List<CartProductDto> cartProducts)
        {
            var serviceResponse = await repo.PlaceOrder(userId, cartProducts);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Sorry, place order failed");
            }
            return Results.Ok(serviceResponse.Data);
        }
        private static async Task<IResult> GetOrdersAsync(IOrderRepository repo, int userId)
        {
            var serviceResponse = await repo.GetOrders(userId);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("You have no order");
            }
            return Results.Ok(serviceResponse.Data);
        }
        private static async Task<IResult> GetOrderItems(IOrderRepository repo, int userId, int orderId)
        {
            var serviceResponse = await repo.GetOrderItems(userId, orderId);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Sorry, order not found");
            }
            return Results.Ok(serviceResponse.Data);
        }

    }
}
