using DataAccess.DataAccess.Models;
using DataAccess.DataAccess.Repositories.Interfaces;
using DataAccess.DataAccess.UnitOfWork;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions
{
    public static class CategoryExtension
    {
        public static WebApplication MapCategoryEndpoints(this WebApplication app)
        {
            app.MapGet("/categories", GetAllCategories);

            return app;
        }

        private static async Task<IResult> GetAllCategories(IUnitOfWork unitOfWork)
        {
            var serviceResponse = await unitOfWork.CategoryRepository.GetAllCategories();
            return serviceResponse.Error ? Results.NotFound("Categories not found") : Results.Ok(serviceResponse);
        }
    }
}
