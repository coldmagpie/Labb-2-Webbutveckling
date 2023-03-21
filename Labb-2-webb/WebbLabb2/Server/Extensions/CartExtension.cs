using DataAccess.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions
{
    public static class CartExtension
    {
        public static WebApplication MapCartEndpoints(this WebApplication app)
        {
            app.MapPost("/cartproducts", GetCartProducts);

            return app;
        }

        private static async Task<IResult> GetCartProducts(ICartRepository repo, List<CartItemDto> items)
        {
            var serviceResponse = await repo.GetCartProducts(items);

            return serviceResponse.Error ? Results.BadRequest("Items not found") : Results.Ok(serviceResponse);
        }
    }
}
