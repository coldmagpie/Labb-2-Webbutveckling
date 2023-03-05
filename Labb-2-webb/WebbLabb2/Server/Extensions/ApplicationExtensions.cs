using DataAccess.DataAccess.Interfaces;
using DataAccess.DataAccess.Models;
using WebbLabb2.Server.Services.AuthService;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions;
public static class ApplicationExtensions
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/user/register", RegisterUserHandlerAsync);
        app.MapPost("/user/login", LoginUserHandlerAsync);

        return app;
    }
    private static async Task<IResult> LoginUserHandlerAsync(IAuthService authService, UserLoginDto dto)
    {
        var response = await authService.LoginUserAsync(dto.Email, dto.Password);
        return response.Error ? Results.BadRequest(response) : Results.Ok(response);
    }

    private static async Task<IResult> RegisterUserHandlerAsync(IAuthService authService, UserRegisterDto dto)
    {
        var model = new UserModel()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Adress = dto.Adress
        };
        var response = await authService.RegisterUserAsync(model, dto.Password);
        return response.Error ? Results.BadRequest(response) : Results.Ok(response);
    }


    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app.MapPost("/createproduct", CreateProductAsync);
        app.MapGet("/products", GetProductsAsync);
        app.MapGet("/products/{id}", GetProductById);
        app.MapGet("/productname/{name}", GetProductByNameAsync);
        app.MapGet("/productnumber/{number}", GetProductByNumberAsync);
        app.MapGet("/categoryproducts/{category}", GetProductByCategoryAsync);
        app.MapGet("/searchproduct/{text}", GetProductBySearchText);
        app.MapPatch("/updateproduct/{id}", UpdateProductAsync);
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

