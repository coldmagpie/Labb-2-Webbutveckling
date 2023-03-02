using MongoDB.Bson;

public interface ICategoryRepository<T, TU>
{
    Task<ServiceResponse<T>> AddCategoryAsync(TU item);
    Task<ServiceResponse<T>> DeleteCategoryAsync(ObjectId id);
    Task<ServiceResponse<TU>> UpdateCategoryAsync(ObjectId id, TU item);
    Task<T> GetCategoryByName(string name);
}

