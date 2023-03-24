using WebbLabb2.Shared;

namespace DataAccess.DataAccess.Repositories.CategoryRepository;

public interface ICategoryRepository<T, TU>
{
    Task<ServiceResponse<List<T>>> GetAllCategories();
    Task<ServiceResponse<T>> AddCategoryAsync(TU item);
    Task<ServiceResponse<T>> DeleteCategoryAsync(int id);
    Task<ServiceResponse<T>> UpdateCategoryAsync(int id, TU item);
    //Task<T> GetCategoryByName(string name);
}