
using WebbLabb2.Shared;

namespace DataAccess.DataAccess.Interfaces;

public interface IProductRepository<T, TU>
{
    Task <ServiceResponse<T>> AddProductAsync(TU item);
    Task <ServiceResponse<T>>DeleteProductAsync(int id);
    Task<ServiceResponse<T>> GetProductByNumberAsync(string number);
    Task<ServiceResponse<T>> GetProductByIdAsync(int id);
    Task<ServiceResponse<T>> GetProductByNameAsync(string name);
    Task<ServiceResponse<List<T>>> GetProductsByCategory(string name);
    Task<ServiceResponse<List<T>>> GetAllAsync();
    Task<ServiceResponse<T>> UpdateAsync(int id, TU item);
    Task<ServiceResponse<List<T>>> GetProductsBySearchText(string text);
}