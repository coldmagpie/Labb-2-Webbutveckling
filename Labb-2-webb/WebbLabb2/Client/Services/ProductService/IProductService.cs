using DataAccess.DataAccess.Models;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.ProductService;

public interface IProductService
{
    string Message { get; set; }
    public List<ProductDto> Products { get; set; }

    public Task<ProductModel> CreateProduct(ProductDto dto);
    public Task GetProducts(string ? category = null);
    public Task <ProductDto> GetProductById(int id);
    public Task GetProductBySearchText (string text);
    public Task <ProductModel> UpdateProduct(int id, ProductDto dto);
    public Task DeleteProductAsync(int id);

    event Action ProductsChanged;
}