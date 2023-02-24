public interface IProductRepository<T, TU>
{
    Task <ServiceResponse<TU>> AddAsync(T item);
    Task <ServiceResponse<TU>>DeleteAsync(object id);
    Task<ServiceResponse<TU>> GetProductByNumberAsync(string number);
    Task<ServiceResponse<TU>> GetProductByNameAsync(string name);
    Task<ServiceResponse<IEnumerable<TU>>> GetAllAsync();
    Task<ServiceResponse<TU>> UpdateAsync(object id, T item);
}

