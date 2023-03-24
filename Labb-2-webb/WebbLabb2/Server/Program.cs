using DataAccess.DataAccess.DataContext;
using DataAccess.DataAccess.Interfaces;
using DataAccess.DataAccess.Models;
using DataAccess.DataAccess.Repositories;
using DataAccess.DataAccess.Repositories.CategoryRepository;
using DataAccess.DataAccess.Repositories.OrderRepository;
using DataAccess.DataAccess.Repositories.ProductRepository;
using DataAccess.DataAccess.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Server.Extensions;
using WebbLabb2.Server.Services.AuthService;
using WebbLabb2.Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICategoryRepository<CategoryModel, CategoryDto>, CategoryRepository>();
builder.Services.AddScoped<IProductRepository<ProductModel, ProductDto>, ProductRepository>();
builder.Services.AddScoped<IUserRepository<UserModel>, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddDbContext<StoreContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("StoreDb");
    options.UseSqlServer(connectionString);
});

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

app.UseRouting();

app.MapAuthEndpoints();
app.MapProductEndpoints();
app.MapCategoryEndpoints();
app.MapCartEndpoints();
app.MapOrderEndpoints();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
