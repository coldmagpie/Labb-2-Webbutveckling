using MongoDB.Bson;



public interface IProductRepository<T, TU>
{
    Task <ServiceResponse<TU>> AddProductAsync(T item);
    Task <ServiceResponse<TU>>DeleteProductAsync(ObjectId id);
    Task<ServiceResponse<T>> GetProductByNumberAsync(string number);
    Task<ServiceResponse<T>> GetProductByNameAsync(string name);
    Task<ServiceResponse<List<T>>> GetProductsByCategory(string name);
    Task<ServiceResponse<List<T>>> GetAllAsync();
    Task<ServiceResponse<T>> UpdateAsync(ObjectId id, T item);
    Task<ServiceResponse<List<T>>> GetProductsBySearchText(string text);
}