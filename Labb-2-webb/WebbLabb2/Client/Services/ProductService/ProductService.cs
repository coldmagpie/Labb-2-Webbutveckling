using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Xml.Linq;
using DataAccess.DataAccess.Models;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    public event Action ProductsChanged;
    public string Message { get; set; } = "We work hard to get the products....";
    public List<ProductDto> Products { get; set; } = new ();
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetProducts(string ? category = null)
    {
        var result = category is null?
            await _httpClient.GetFromJsonAsync<List<ProductModel>>("/products"):
            await _httpClient.GetFromJsonAsync<List<ProductModel>>($"/categoryproducts/{category}");

        Products = result.Select(p=>new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Number = p.Number,
            Description = p.Description,
            Price = p.Price,
            Image = p.ImageUrl,
            IsWeightable = p.IsWeightable,
            Status = p.Status
        }).ToList();
        ProductsChanged?.Invoke();
    }

    public async Task<List<ProductDto>> GetProductByCategory(string? category)
    {
        var result = await _httpClient.GetFromJsonAsync<List<ProductModel>>($"/categoryproducts/{category}");
       
            Products = result.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Number = p.Number,
                Description = p.Description,
                Price = p.Price,
                Image = p.ImageUrl,
                IsWeightable = p.IsWeightable,
                Status = p.Status
            }).ToList();
            ProductsChanged.Invoke();
            return Products;
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<ProductModel>($"/products/{id}");
        var product = new ProductDto
        {
            Id = result.Id,
            Name = result.Name,
            Number = result.Number,
            Description = result.Description,
            Price = result.Price,
            Image = result.ImageUrl,
            IsWeightable = result.IsWeightable,
            Status = result.Status
        };
        return product;
    }

    public async Task <ProductDto> GetProductByName(string name)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductModel>>($"/productname/{name}");
        if (result.Error)
        {
            result.Message = "Product not found";
        }
        var product = new ProductDto
        {
            Id = result.Data.Id,
            Name = result.Data.Name,
            Number = result.Data.Number,
            Description = result.Data.Description,
            Price = result.Data.Price,
            Image = result.Data.ImageUrl,
            IsWeightable = result.Data.IsWeightable,
            Status = result.Data.Status
        };

        return product;
    }
    public async Task <ProductDto> GetProductByNumber(string number)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductModel>>($"/productnumber/{number}");
        if (result.Error)
        {
            result.Message = "Product not found";
        }
        var product = new ProductDto
        {
            Id = result.Data.Id,
            Name = result.Data.Name,
            Number = result.Data.Number,
            Description = result.Data.Description,
            Price = result.Data.Price,
            Image = result.Data.ImageUrl,
            IsWeightable = result.Data.IsWeightable,
            Status = result.Data.Status
        };

        return product;
    }

    public async Task DeleteProductAsync(int id)
    {
        var result = await _httpClient.DeleteFromJsonAsync<ServiceResponse<ProductModel>>($"/deleteproduct/{id}");
        if (result.Error)
        {
            result.Message = "Product not found";
        }
        ProductsChanged.Invoke();
    }

    public async Task GetProductBySearchText(string text)
    {
        var result = await _httpClient.GetFromJsonAsync<List<ProductModel>>($"/searchproduct/{text}");
      
        Products = result.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Number = p.Number,
            Description = p.Description,
            Price = p.Price,
            Image = p.ImageUrl,
            IsWeightable = p.IsWeightable,
            Status = p.Status
        }).ToList();
        ProductsChanged?.Invoke();
    }
}