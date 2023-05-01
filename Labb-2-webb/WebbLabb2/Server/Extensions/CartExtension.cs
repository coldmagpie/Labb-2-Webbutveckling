using DataAccess.DataAccess.Repositories.Interfaces;
using DataAccess.DataAccess.UnitOfWork;
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

        private static async Task<IResult> GetCartProducts(IUnitOfWork unitOfWork, List<CartItemDto> items)
        {
            var serviceResponse = await unitOfWork.CartRepository.GetCartProducts(items);

            return serviceResponse.Error ? Results.NotFound("Items not found") : Results.Ok(serviceResponse);
        }
    }
}
