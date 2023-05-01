using DataAccess.DataAccess.Models;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<ServiceResponse<List<CategoryModel>>> GetAllCategories();
    Task<ServiceResponse<CategoryModel>> AddCategoryAsync(CategoryModel item);
    Task<ServiceResponse<CategoryModel>> DeleteCategoryAsync(int id);
    Task<ServiceResponse<CategoryModel>> UpdateCategoryAsync(int id, CategoryModel item);
}