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
    public string Message { get; set; } = "We work hard to get products...";
    public List<ProductDto> Products { get; set; } = new ();
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductModel> CreateProduct(ProductDto dto)
    {
        var result =await _httpClient.PostAsJsonAsync("/createproduct", dto);
        var product = new ProductModel()
        {
            Name = dto.Name,
            Number = dto.Number,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            IsWeightable = dto.IsWeightable,
            InStock = dto.InStock
        };
        return product;
    }

    public async Task GetProducts(string ? category = null)
    {
        var result = category is null?
            await _httpClient.GetFromJsonAsync<List<ProductDto>>("/products"):
            await _httpClient.GetFromJsonAsync<List<ProductDto>>($"/categoryproducts/{category}");

        if (result != null) Products = result.ToList();
        ProductsChanged?.Invoke();
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<ProductDto>($"/products/{id}");
        var product = new ProductDto
        {
            Id = result.Id,
            Name = result.Name,
            Number = result.Number,
            Description = result.Description,
            Price = result.Price,
            ImageUrl = result.ImageUrl,
            IsWeightable = result.IsWeightable,
            InStock = result.InStock
        };
        return product;
    }

    public async Task GetProductBySearchText(string text)
    {
        var result = await _httpClient.GetFromJsonAsync<List<ProductDto>>($"/searchproduct/{text}");

        if (result != null) Products = result.ToList();
        ProductsChanged?.Invoke();
    }

    public async Task<ProductModel> UpdateProduct(int id, ProductDto dto)
    {
        var result = await _httpClient.PutAsJsonAsync($"/product/update/{id}", dto);
        var product = new ProductModel()
        {
            Name = dto.Name,
            Number = dto.Number,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            IsWeightable = dto.IsWeightable,
            InStock = dto.InStock
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

}