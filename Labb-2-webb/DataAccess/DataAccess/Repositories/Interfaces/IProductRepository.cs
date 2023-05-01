using DataAccess.DataAccess.Models;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.Repositories.Interfaces;

public interface IProductRepository
{
    Task<ServiceResponse<ProductModel>> AddProductAsync(ProductModel item);
    Task<ServiceResponse<ProductModel>> DeleteProductAsync(int id);
    Task<ServiceResponse<ProductModel>> GetProductByIdAsync(int id);
    Task<ServiceResponse<List<ProductModel>>> GetProductsByCategory(string name);
    Task<ServiceResponse<List<ProductModel>>> GetAllAsync();
    Task<ServiceResponse<ProductModel>> UpdateAsync(int id, ProductModel item);
    Task<ServiceResponse<List<ProductModel>>> GetProductsBySearchText(string text);
}