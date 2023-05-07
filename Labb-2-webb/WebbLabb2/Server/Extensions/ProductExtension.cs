using Azure;
using DataAccess.DataAccess.Models;
using DataAccess.DataAccess.Repositories.Interfaces;
using DataAccess.DataAccess.UnitOfWork;
using DataAccess.Migrations;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions
{
    public static class ProductExtension
    {
        public static WebApplication MapProductEndpoints(this WebApplication app)
        {
            app.MapPost("/createproduct", CreateProductAsync).RequireAuthorization("Administrator");
            app.MapGet("/products", GetProductsAsync);
            app.MapGet("/products/{id}", GetProductById);
            app.MapGet("/categoryproducts/{category}", GetProductByCategoryAsync);
            app.MapGet("/searchproduct/{text}", GetProductBySearchText);
            app.MapPut("/product/update/{id}", UpdateProductAsync).RequireAuthorization("Administrator");
            app.MapDelete("/deleteproduct/{id}", DeleteProductAsync).RequireAuthorization("Administrator");

            return app;
        }

        private static async Task<IResult> GetProductById(IUnitOfWork unitOfWork, int id)
        {
            var response = await unitOfWork.ProductRepository.GetProductByIdAsync(id);
            return response.Error ? Results.NotFound("OOOps! this product doesn't exist!") : Results.Ok(response);
        }

        private static async Task<IResult> CreateProductAsync(IUnitOfWork unitOfWork, ProductModel product)
        {
            var response = await unitOfWork.ProductRepository.AddProductAsync(product);
            return response.Error ? Results.BadRequest("Add product failed!") : Results.Ok(response);
        }

        private static async Task<IResult> GetProductsAsync(IUnitOfWork unitOfWork)
        {
            var response = await unitOfWork.ProductRepository.GetAllAsync();
            return response.Error ? Results.NotFound("Product Not found") : Results.Ok(response);
        }

        private static async Task<IResult> GetProductByCategoryAsync(IUnitOfWork unitOfWork, string category)
        {

            var response = await unitOfWork.ProductRepository.GetProductsByCategory(category);
            return response.Error ? Results.BadRequest("Products Not found") : Results.Ok(response);
        }

        private static async Task<IResult> GetProductBySearchText(IUnitOfWork unitOfWork, string text)
        {
            var response = await unitOfWork.ProductRepository.GetProductsBySearchText(text);
            return response.Error ? Results.NotFound("Product Not found") : Results.Ok(response);
        }

        private static async Task<IResult> UpdateProductAsync(IUnitOfWork unitOfWork, int id, ProductModel product)
        {
            var response = await unitOfWork.ProductRepository.UpdateAsync(id, product);
            return response.Error ? Results.BadRequest("Updating failed") : Results.Ok(response);
        }

        private static async Task<IResult> DeleteProductAsync(IUnitOfWork unitOfWork, int id)
        {
            var response = await unitOfWork.ProductRepository.DeleteProductAsync(id);
            return response.Error ? Results.BadRequest("Deleting failed") : Results.Ok(response);
        }
    }
}
