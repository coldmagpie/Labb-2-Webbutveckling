using MongoDB.Bson;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Labb_2_webb.Server.Extensions;
public static class ApplicationExtensions
{
    //public static WebApplication MapAuthEndpoints(this WebApplication app)
    //{
    //    app.MapPost("/user/register", RegisterUserHandlerAsync);
    //    app.MapPost("/user/login", LoginUserHandlerAsync);

    //    return app;
    //}
    //private static async Task<IResult> LoginUserHandlerAsync(IAuthService authService, UserLoginDto dto)
    //{
    //    var response = await authService.LoginUserAsync(dto.Email, dto.Password);
    //    return response.Success ? Results.Ok(response) : Results.BadRequest(response);
    //}

    //private static async Task<IResult> RegisterUserHandlerAsync(IAuthService authService, UserRegisterDto dto)
    //{
    //    var model = new UserModel()
    //    {
    //        FirstName = dto.FirstName, 
    //        LastName = dto.LastName,
    //        Email = dto.Email,
    //        PhoneNumber = dto.PhoneNumber,
    //        Adress = dto.Adress
    //    };
    //    var response = await authService.RegisterUserAsync(model, dto.Password);
    //    return response.Success ? Results.Ok(response) : Results.BadRequest(response);
    //}


    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app.MapPost("/createproduct", CreateProductAsync);
        app.MapGet("/products", GetProductsAsync);
        app.MapGet("/productname/{name}", GetProductByNameAsync);
        app.MapGet("/productnumber/{number}", GetProductByNumberAsync);
        app.MapGet("/categoryproduct/{category}", GetProductByCategoryAsync);
        app.MapGet("/searchproduct/{text}", GetProductBySearchText);
        app.MapPatch("/updateproduct/{id}", UpdateProductAsync);
        app.MapDelete("/deleteproduct/{id}", DeleteProductAsync);

        return app;
    }

    private static async Task<IResult> CreateProductAsync(IProductRepository<ProductDto, ProductModel> repo, ProductDto dto)
    {
        var serviceResponse = await repo.AddProductAsync(dto);
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }
    private static async Task<IResult> GetProductsAsync(IProductRepository<ProductDto, ProductModel> repo)
    {
        var serviceResponse = await repo.GetAllAsync();
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }

    private static async Task<IResult> GetProductByNameAsync(IProductRepository<ProductDto, ProductModel> repo, string name)
    {
        var serviceResponse = await repo.GetProductByNameAsync(name);
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }

    private static async Task<IResult> GetProductByNumberAsync(IProductRepository<ProductDto, ProductModel> repo, string number)
    {
        var serviceResponse = await repo.GetProductByNumberAsync(number);
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }
    private static async Task<IResult> GetProductByCategoryAsync(IProductRepository<ProductDto, ProductModel> repo, string category)
    {

        var serviceResponse = await repo.GetProductsByCategory(category);
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }

    private static async Task<IResult> GetProductBySearchText(IProductRepository<ProductDto, ProductModel> repo, string text)
    {
        var serviceResponse = await repo.GetProductsBySearchText(text);
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }

    private static async Task<IResult> UpdateProductAsync(IProductRepository<ProductDto, ProductModel> repo, string id, ProductDto dto)
    {
        var serviceResponse = await repo.UpdateAsync(ObjectId.Parse(id), dto);
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }

    private static async Task<IResult> DeleteProductAsync(IProductRepository<ProductDto, ProductModel> repo, string id)
    {
        var serviceResponse = await repo.DeleteProductAsync(ObjectId.Parse(id));
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }

    public static WebApplication MapCategoryEndpoints(this WebApplication app)
    {
        app.MapPost("/createcategory", CreateCategory);
        app.MapPatch("/updatecategory/{id}", UpdateCategory);
        app.MapDelete("/deletecategory/{id}", DeleteCategory);

        return app;
    }

    private static async Task<IResult> CreateCategory(ICategoryRepository<CategoryModel, CategoryDto> repo, CategoryDto dto)
    {
        var serviceResponse = await repo.AddCategoryAsync(dto);
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }
    private static async Task<IResult> UpdateCategory(ICategoryRepository<CategoryModel, CategoryDto> repo, string id, CategoryDto dto)
    {
        var serviceResponse = await repo.UpdateCategoryAsync(ObjectId.Parse(id), dto);
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }
    private static async Task<IResult> DeleteCategory(ICategoryRepository<CategoryModel, CategoryDto> repo, string id)
    {
        var serviceResponse = await repo.DeleteCategoryAsync(ObjectId.Parse(id));
        if (!serviceResponse.Success)
        {
            return Results.BadRequest(serviceResponse.Message);
        }
        return Results.Ok(serviceResponse.Data);
    }
}

