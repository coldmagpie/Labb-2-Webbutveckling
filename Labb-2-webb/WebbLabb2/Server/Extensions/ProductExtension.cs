using Azure;
using DataAccess.DataAccess.Interfaces;
using DataAccess.DataAccess.Models;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions
{
    public static class ProductExtension
    {
        public static WebApplication MapProductEndpoints(this WebApplication app)
        {
            app.MapPost("/createproduct", CreateProductAsync);
            app.MapGet("/products", GetProductsAsync);
            app.MapGet("/products/{id}", GetProductById);
            app.MapGet("/categoryproducts/{category}", GetProductByCategoryAsync);
            app.MapGet("/searchproduct/{text}", GetProductBySearchText);
            app.MapPut("/product/update/{id}", UpdateProductAsync);
            app.MapDelete("/deleteproduct/{id}", DeleteProductAsync);

            return app;
        }

        private static async Task<IResult> GetProductById(IProductRepository<ProductModel, ProductDto> repo, int id)
        {
            var response = await repo.GetProductByIdAsync(id);
            return response.Error ? Results.BadRequest("OOOps! this product doesn't exist!") : Results.Ok(response);
        }

        private static async Task<IResult> CreateProductAsync(IProductRepository<ProductModel, ProductDto> repo, ProductDto dto)
        {
            var response = await repo.AddProductAsync(dto);
            return response.Error ? Results.BadRequest("Add product failed!") : Results.Ok(response);
        }

        private static async Task<IResult> GetProductsAsync(IProductRepository<ProductModel, ProductDto> repo)
        {
            var response = await repo.GetAllAsync();
            return response.Error ? Results.BadRequest("Product Not found") : Results.Ok(response);
        }

        private static async Task<IResult> GetProductByCategoryAsync(IProductRepository<ProductModel, ProductDto> repo, string category)
        {

            var response = await repo.GetProductsByCategory(category);
            return response.Error ? Results.BadRequest("Products Not found") : Results.Ok(response);
        }

        private static async Task<IResult> GetProductBySearchText(IProductRepository<ProductModel, ProductDto> repo, string text)
        {
            var response = await repo.GetProductsBySearchText(text);
            return response.Error ? Results.BadRequest("Product Not found") : Results.Ok(response);
        }

        private static async Task<IResult> UpdateProductAsync(IProductRepository<ProductModel, ProductDto> repo, int id, ProductDto dto)
        {
            var response = await repo.UpdateAsync(id, dto);
            return response.Error ? Results.BadRequest("Updating failed") : Results.Ok(response);
        }

        private static async Task<IResult> DeleteProductAsync(IProductRepository<ProductModel, ProductDto> repo, int id)
        {
            var response = await repo.DeleteProductAsync(id);
            return response.Error ? Results.BadRequest("Deleting failed") : Results.Ok(response);
        }
    }
}
