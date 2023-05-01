using DataAccess.DataAccess.DataContext;
using DataAccess.DataAccess.Models;
using DataAccess.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public StoreContext _storeContext;

    public CategoryRepository(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    public async Task<ServiceResponse<List<CategoryModel>>> GetAllCategories()
    {
        var response = new ServiceResponse<List<CategoryModel>>();
        var categories = await _storeContext.Categories.ToListAsync();
        if (categories is null)
        {
            response.Error = true;
            response.Message = "Categories not found";
        }
        else
        {
            response.Error = false;
            response.Data = categories;
        }
        return response;
    }

    public async Task<ServiceResponse<CategoryModel>> AddCategoryAsync(CategoryModel category)
    {
        var response = new ServiceResponse<CategoryModel>();
        var exist = await _storeContext.Categories.AnyAsync(c => c.Name.Equals(category.Name));
        if (exist)
        {
            response.Error = true;
            response.Message = $"Category already exists!";
            return response;
        }

        await _storeContext.Categories.AddAsync(category);
        await _storeContext.SaveChangesAsync();
        response.Error = false;
        response.Data = category;

        return response;
    }

    public async Task<ServiceResponse<CategoryModel>> DeleteCategoryAsync(int id)
    {
        var response = new ServiceResponse<CategoryModel>();
        var category = await _storeContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category is null)
        {
            response.Error = true;
            response.Message = $"Sorry, this category doesn't exist";
        }
        else
        {
            _storeContext.Categories.Remove(category);
            await _storeContext.SaveChangesAsync();
            response.Error = true;
            response.Message = $"Product was deleted successfully";
        }
        return response;
    }

    public async Task<ServiceResponse<CategoryModel>> UpdateCategoryAsync(int id, CategoryModel dto)
    {
        var response = new ServiceResponse<CategoryModel>();
        var category = await _storeContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category is null)
        {
            response.Error = true;
            response.Message = $"Sorry, this category doesn't exist";
        }
        else
        {
            category.Name = dto.Name;
            await _storeContext.SaveChangesAsync();
            response.Error = false;
            response.Data = new CategoryModel() { Name = category.Name };
        }

        return response;
    }
}