using DataAccess.DataAccess.DataContext;
using Labb_2_webb.Server.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Server.Services.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICategoryRepository<CategoryModel, CategoryDto>, CategoryRepository>();
builder.Services.AddScoped<IProductRepository<ProductDto, ProductModel>, ProductRepository>();

builder.Services.AddDbContext<UserContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("StoreDb");
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IAuthService, AuthService>();

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

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
