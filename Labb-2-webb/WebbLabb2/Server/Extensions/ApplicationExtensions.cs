using System.Security.Claims;
using DataAccess.DataAccess.Interfaces;
using DataAccess.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbLabb2.Shared.DTOs;
using Microsoft.AspNetCore.Http;

using IAuthService = WebbLabb2.Server.Services.AuthService.IAuthService;
using WebbLabb2.Shared;
using System.Net.Http;

namespace WebbLabb2.Server.Extensions;
public static class ApplicationExtensions
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/user/register", RegisterUserHandlerAsync);
        app.MapPost("/user/login", LoginUserHandlerAsync);
        app.MapPost("/user/update/{id}", UpdateHandlerAsync);
        app.MapPost("/user/changepassword/{id}", ChangePasswordAsync);
        app.MapGet("/userid/{id}", GetUserById);
        app.MapGet("/useremail/{email}", GetUserByEmail);
        app.MapGet("/allusers", GetAllUsers);
        //app.MapGet("/user{userId}/order{orderId}/items", GetOrderItems);

        return app;
    }

    private static async Task<IResult> LoginUserHandlerAsync(IAuthService authService, UserLoginDto dto)
    {
        var response = await authService.LoginUserAsync(dto.Email, dto.Password);
        return response.Error ? Results.BadRequest(response) : Results.Ok(response);
    }

    private static async Task<IResult> RegisterUserHandlerAsync(IAuthService authService, UserRegisterDto dto)
    {
        var model = new UserModel
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

    
    public static async Task<IResult> UpdateHandlerAsync(IAuthService authService, int id, UserProfileDto newProfile)
    {
        var response = await authService.UpdateProfile(id, newProfile);

        if (response.Error)
        {
            return Results.BadRequest(response);
        }
        return Results.Ok(response);
    }

    public static async Task<IResult> ChangePasswordAsync(IAuthService authService,[FromBody] string password, int id)
    {
        var response = await authService.ChangePassword(id, password);
        if (response.Error)
        {
            return Results.BadRequest(response);
        }

        return Results.Ok(response);
    }
    private static async Task<IResult> GetUserById(IUserRepository<UserModel> userRepository, int id)
    {
        var response = await userRepository.GetUserById(id);
        return response.Error ? Results.BadRequest(response) : Results.Ok(response.Data);
    }
    private static async Task<IResult> GetUserByEmail(IUserRepository<UserModel> userRepository,string email)
    {
        var response = await userRepository.GetUserByEmail(email);
        return response.Error ? Results.BadRequest(response) : Results.Ok(response.Data);
    }
    private static async Task<IResult> GetAllUsers(IUserRepository<UserModel> userRepository)
    {
        var response = await userRepository.GetAllUsers();
        return response.Error ? Results.BadRequest(response) : Results.Ok(response.Data);
    }
    //private static async Task<IResult> GetOrderItems(IUserRepository<UserModel> userRepository, int userId, int orderId)
    //{
    //    var response = await userRepository.GetUserOrderItems(userId, orderId);
    //    return response.Error ? Results.BadRequest(response) : Results.Ok(response.Data);
    //}
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

