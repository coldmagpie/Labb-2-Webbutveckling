using DataAccess.DataAccess.DataContext;
using DataAccess.DataAccess.Models;
using DataAccess.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _storeContext;

    public ProductRepository(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    public async Task<ServiceResponse<ProductModel>> AddProductAsync(ProductModel dto)
    {
        var response = new ServiceResponse<ProductModel>();
        var exist = await _storeContext.Products.AnyAsync(p => p.Name.Equals(dto.Name) || p.Number.Equals(dto.Number));
        if (exist)
        {
            response.Error = true;
            response.Message = "This product already exists";
        }

        await _storeContext.Products.AddAsync(dto);
        await _storeContext.SaveChangesAsync();
        return new ServiceResponse<ProductModel>
        {
            Error = false,
            Message = "Product added!",
            Data = dto
        };
    }


    public async Task<ServiceResponse<List<ProductModel>>> GetAllAsync()
    {
        var response = new ServiceResponse<List<ProductModel>>();
        var products = await _storeContext.Products.ToListAsync();
        if (products is null)
        {
            response.Error = true;
            response.Message = $"Sorry, this is no product";
        }
        else
        {
            response.Error = false;
            response.Data = products;
        }

        return response;
    }


    public async Task<ServiceResponse<ProductModel>> GetProductByIdAsync(int id)
    {

        var response = new ServiceResponse<ProductModel>();
        var product = await _storeContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
        {
            response.Error = true;
            response.Message = $"Sorry, this product doesn't exist";
        }
        else
        {
            response.Error = false;
            response.Data = product;
        }

        return response;
    }

    public async Task<ServiceResponse<List<ProductModel>>> GetProductsByCategory(string name)
    {
        var category = await _storeContext.Categories.FirstOrDefaultAsync(c => c.Name.Equals(name));
        if (category is null)
        {
            return new ServiceResponse<List<ProductModel>>
            {
                Error = true,
                Message = "Sorry, category not found!"
            };
        }

        var productList = await _storeContext.Products.Where(p => p.CategoryId == category.Id).ToListAsync();
        return new ServiceResponse<List<ProductModel>>
        {
            Error = false,
            Data = productList
        };
    }

    public async Task<ServiceResponse<List<ProductModel>>> GetProductsBySearchText(string text)
    {
        var response = new ServiceResponse<List<ProductModel>>();
        var products = await _storeContext.Products
            .Where(p => p.Name.ToLower().Contains(text.ToLower()) ||
                        p.Description.ToLower().Contains(text.ToLower()) || p.Number.Contains(text))
            .ToListAsync();
        response.Error = false;
        response.Data = products;

        return response;
    }

    public async Task<ServiceResponse<ProductModel>> UpdateAsync(int id, ProductModel newProduct)
    {
        var response = new ServiceResponse<ProductModel>();
        var product = await _storeContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
        {

            response.Error = true;
            response.Message = $"Sorry, this product doesn't exist";

        }
        else
        {
            product.Number = newProduct.Number;
            product.Name = newProduct.Name;
            product.CategoryId = newProduct.CategoryId;
            product.Description = newProduct.Description;
            product.IsWeightable = newProduct.IsWeightable;
            product.ImageUrl = newProduct.ImageUrl;
            product.Price = newProduct.Price;
            product.InStock = newProduct.InStock;
            await _storeContext.SaveChangesAsync();
        }

        return response;
    }
    public async Task<ServiceResponse<ProductModel>> DeleteProductAsync(int id)
    {
        var response = new ServiceResponse<ProductModel>();
        var product = await _storeContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
        {
            response.Error = true;
            response.Message = $"Sorry, this product doesn't exist";
        }
        else
        {
            _storeContext.Products.Remove(product);
            await _storeContext.SaveChangesAsync();
            response.Error = false;
            response.Message = $"Product was deleted successfully";
        }
        return response;
    }
}