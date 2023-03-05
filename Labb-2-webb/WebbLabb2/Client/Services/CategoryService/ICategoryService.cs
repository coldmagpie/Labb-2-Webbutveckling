using DataAccess.DataAccess.Models;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action OnChange;
        List<CategoryDto> Categories { get; set; }
        Task <List<CategoryDto>> GetAllCategories();
        //Task AddCategory(CategoryDto category);
        //Task UpdateCategory(CategoryDto category);
        //Task DeleteCategory(int categoryId);
        //Task CreateNewCategory(CategoryDto category);
    }
}
