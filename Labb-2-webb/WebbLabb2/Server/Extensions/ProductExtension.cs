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
            app.MapGet("/productname/{name}", GetProductByNameAsync);
            app.MapGet("/productnumber/{number}", GetProductByNumberAsync);
            app.MapGet("/categoryproducts/{category}", GetProductByCategoryAsync);
            app.MapGet("/searchproduct/{text}", GetProductBySearchText);
            app.MapPut("/product/update/{id}", UpdateProductAsync);
            app.MapDelete("/deleteproduct/{id}", DeleteProductAsync);

            return app;
        }

        private static async Task<IResult> GetProductById(IProductRepository<ProductModel, ProductDto> repo, int id)
        {
            var serviceResponse = await repo.GetProductByIdAsync(id);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("OOOps! this product doesn't exist!");
            }
            return Results.Ok(serviceResponse.Data);
        }

        private static async Task<IResult> CreateProductAsync(IProductRepository<ProductModel, ProductDto> repo, ProductDto dto)
        {
            var serviceResponse = await repo.AddProductAsync(dto);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Add product failed!");
            }
            return Results.Ok(serviceResponse.Data);
        }
        private static async Task<IResult> GetProductsAsync(IProductRepository<ProductModel, ProductDto> repo)
        {
            var serviceResponse = await repo.GetAllAsync();
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Product Not found");
            }
            return Results.Ok(serviceResponse.Data);
        }

        private static async Task<IResult> GetProductByNameAsync(IProductRepository<ProductModel, ProductDto> repo, string name)
        {
            var serviceResponse = await repo.GetProductByNameAsync(name);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Product Not found");
            }
            return Results.Ok(serviceResponse.Data);
        }

        private static async Task<IResult> GetProductByNumberAsync(IProductRepository<ProductModel, ProductDto> repo, string number)
        {
            var serviceResponse = await repo.GetProductByNumberAsync(number);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Product Not found");
            }
            return Results.Ok(serviceResponse.Data);
        }
        private static async Task<IResult> GetProductByCategoryAsync(IProductRepository<ProductModel, ProductDto> repo, string category)
        {

            var serviceResponse = await repo.GetProductsByCategory(category);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Products Not found");
            }
            return Results.Ok(serviceResponse.Data);
        }

        private static async Task<IResult> GetProductBySearchText(IProductRepository<ProductModel, ProductDto> repo, string text)
        {
            var serviceResponse = await repo.GetProductsBySearchText(text);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Product Not found");
            }
            return Results.Ok(serviceResponse.Data);
        }

        private static async Task<IResult> UpdateProductAsync(IProductRepository<ProductModel, ProductDto> repo, int id, ProductDto dto)
        {
            var serviceResponse = await repo.UpdateAsync(id, dto);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Updating failed");
            }
            return Results.Ok(serviceResponse.Data);
        }

        private static async Task<IResult> DeleteProductAsync(IProductRepository<ProductModel, ProductDto> repo, int id)
        {
            var serviceResponse = await repo.DeleteProductAsync(id);
            if (serviceResponse.Error)
            {
                return Results.BadRequest("Deleting failed");
            }
            return Results.Ok(serviceResponse.Data);
        }
    }
}
