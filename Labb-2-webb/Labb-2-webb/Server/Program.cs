using Microsoft.AspNetCore.ResponseCompression;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IProductRepository<ProductDto, ProductModel>, ProductRepository>();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.MapPost("/createproduct", async (IProductRepository<ProductDto, ProductModel> repo, ProductDto dto) =>
{
    var serviceResponse = await repo.AddAsync(dto);
    if (!serviceResponse.Success)
    {
        return Results.BadRequest(serviceResponse.Message);
    }
    return Results.Ok(serviceResponse.Data);
  
});
app.MapGet("/getproducts", async (IProductRepository<ProductDto, ProductModel> repo) =>
{
    var serviceResponse = await repo.GetAllAsync();
    if (serviceResponse.Success)
    {
        return Results.Ok(serviceResponse.Data);
    }
    return Results.BadRequest(serviceResponse.Message);
});

app.MapGet("/getproduct/{name}", async (IProductRepository<ProductDto, ProductModel> repo, string name) =>
{
    var serviceResponse = await repo.GetProductByNameAsync(name);
    if (!serviceResponse.Success)
    {
        return Results.BadRequest(serviceResponse.Message);
    }
    return Results.Ok(serviceResponse.Data);
});
app.MapGet("/getproduct/{number}", async (IProductRepository<ProductDto, ProductModel> repo, string number) =>
{
    var serviceResponse = await repo.GetProductByNameAsync(number);
    if (!serviceResponse.Success)
    {
        return Results.BadRequest(serviceResponse.Message);
    }
    return Results.Ok(serviceResponse.Data);
});


app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
