using DataAccess.DataAccess.Repositories.Interfaces;
using DataAccess.DataAccess.UnitOfWork;
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

        private static async Task<IResult> GetOrderById(IUnitOfWork unitOfWork, int userId, int orderId)
        {
            var response = await unitOfWork.OrderRepository.GetOrderById(userId, orderId);
            return response.Error ? Results.BadRequest("Sorry, order not found") : Results.Ok(response);
        }

        private static async Task<IResult> PlaceOrderAsync(IUnitOfWork unitOfWork, int userId, List<CartProductDto> cartProducts)
        {
            var response = await unitOfWork.OrderRepository.PlaceOrder(userId, cartProducts);
            return response.Error ? Results.BadRequest("Sorry, place order failed") : Results.Ok(response);
        }
        private static async Task<IResult> GetOrdersAsync(IUnitOfWork unitOfWork, int userId)
        {
            var response = await unitOfWork.OrderRepository.GetOrders(userId);
            return response.Error ? Results.BadRequest("something went wrong") : Results.Ok(response);
        }
        private static async Task<IResult> GetOrderItems(IUnitOfWork unitOfWork, int userId, int orderId)
        {
            var response = await unitOfWork.OrderRepository.GetOrderItems(userId, orderId);
            return response.Error ? Results.NotFound("Sorry, order not found") : Results.Ok(response);
        }

    }
}
