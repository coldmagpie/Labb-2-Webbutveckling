public interface IProductService<T>
{
    public Task GetProductByCategory(string category);
    public Task<ServiceResponse<T>> GetProductByName(string name);
    public Task<ServiceResponse<T>> GetProductByNumber(string number);
    public Task GetProductBySearchText(string text);
}

