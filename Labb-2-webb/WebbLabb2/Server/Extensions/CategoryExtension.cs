using DataAccess.DataAccess.Models;
using DataAccess.DataAccess.Repositories.CategoryRepository;
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

        private static async Task<IResult> GetAllCategories(ICategoryRepository<CategoryModel, CategoryDto> repo)
        {
            var serviceResponse = await repo.GetAllCategories();
            return serviceResponse.Error ? Results.BadRequest("Categories not found") : Results.Ok(serviceResponse);
        }
    }
}
