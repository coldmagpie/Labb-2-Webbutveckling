using MongoDB.Bson;

namespace Labb_2_webb.Server.Extensions;
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
        return response.Success ? Results.Ok(response) : Results.BadRequest(response);
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
        return response.Success ? Results.Ok(response) : Results.BadRequest(response);
    }


    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products", async (IProductRepository<ProductDto, ProductModel> repo) =>
        {
            var serviceResponse = await repo.GetAllAsync();
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        app.MapPost("/createproduct", async (IProductRepository<ProductDto, ProductModel> repo, ProductDto dto) =>
        {
            var serviceResponse = await repo.AddProductAsync(dto);
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        app.MapGet("/productname/{name}", async (IProductRepository<ProductDto, ProductModel> repo, string name) =>
        {
            var serviceResponse = await repo.GetProductByNameAsync(name);
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        app.MapGet("/productnumber/{number}", async (IProductRepository<ProductDto, ProductModel> repo, string number) =>
        {
            var serviceResponse = await repo.GetProductByNumberAsync(number);
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        app.MapPatch("/updateproduct/{id}", async (IProductRepository<ProductDto, ProductModel> repo, string id, ProductDto dto) =>
        {
            var serviceResponse = await repo.UpdateAsync(ObjectId.Parse(id), dto);
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        app.MapDelete("/deleteproduct/{id}", async (IProductRepository<ProductDto, ProductModel> repo, string id) =>
        {
            var serviceResponse = await repo.DeleteProductAsync(ObjectId.Parse(id));
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        app.MapGet("/categoryproduct/{category}", async (IProductRepository<ProductDto, ProductModel> repo, string category) =>
        {
            var serviceResponse = await repo.GetProductsByCategory(category);
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        app.MapGet("/searchproduct/{text}", async (IProductRepository<ProductDto, ProductModel> repo, string text) =>
        {
            var serviceResponse = await repo.GetProductsBySearchText(text);
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        return app;
    }

    public static WebApplication MapCategoryEndpoints(this WebApplication app)
    {
        app.MapPost("/createcategory", async (ICategoryRepository<CategoryModel, CategoryDto> repo, CategoryDto dto) =>
        {
            var serviceResponse = await repo.AddCategoryAsync(dto);
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        app.MapPatch("/updatecategory/{id}", async (ICategoryRepository<CategoryModel, CategoryDto> repo, string id, CategoryDto dto) =>
        {
            var serviceResponse = await repo.UpdateCategoryAsync(ObjectId.Parse(id), dto);
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        app.MapDelete("/deletecategory/{id}", async (ICategoryRepository<CategoryModel, CategoryDto> repo, string id) =>
        {
            var serviceResponse = await repo.DeleteCategoryAsync(ObjectId.Parse(id));
            if (!serviceResponse.Success)
            {
                return Results.BadRequest(serviceResponse.Message);
            }
            return Results.Ok(serviceResponse.Data);
        });

        return app;
    }
}

