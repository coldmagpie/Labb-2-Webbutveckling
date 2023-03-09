using DataAccess.DataAccess.Interfaces;
using DataAccess.DataAccess.Models;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions
{
    public static class CategoryExtension
    {
        public static WebApplication MapCategoryEndpoints(this WebApplication app)
        {
            app.MapGet("/categories", GetAllCategories);
            app.MapPost("/createcategory", CreateCategory);
            app.MapPatch("/updatecategory/{id}", UpdateCategory);
            app.MapDelete("/deletecategory/{id}", DeleteCategory);

            return app;
        }

        private static async Task<IResult> GetAllCategories(ICategoryRepository<CategoryModel, CategoryDto> repo)
        {
            var serviceResponse = await repo.GetAllCategories();
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Categories not found");
            }
            return Results.Ok(serviceResponse.Data);

        }

        private static async Task<IResult> CreateCategory(ICategoryRepository<CategoryModel, CategoryDto> repo, CategoryDto dto)
        {
            var serviceResponse = await repo.AddCategoryAsync(dto);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Creating failed");
            }
            return Results.Ok(serviceResponse.Data);
        }
        private static async Task<IResult> UpdateCategory(ICategoryRepository<CategoryModel, CategoryDto> repo, int id, CategoryDto dto)
        {
            var serviceResponse = await repo.UpdateCategoryAsync(id, dto);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Updating failed");
            }
            return Results.Ok(serviceResponse.Data);
        }
        private static async Task<IResult> DeleteCategory(ICategoryRepository<CategoryModel, CategoryDto> repo, int id)
        {
            var serviceResponse = await repo.DeleteCategoryAsync(id);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Deleting failed");
            }
            return Results.Ok(serviceResponse.Data);
        }
    }
}
