using DataAccess.DataAccess.DataContext;
using DataAccess.DataAccess.Interfaces;
using DataAccess.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.Repositories;

public class ProductRepository : IProductRepository<ProductModel, ProductDto>
{
    private readonly StoreContext _storeContext;

    public ProductRepository(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    public async Task<ServiceResponse<ProductModel>> AddProductAsync(ProductDto dto)
    {
        var response = new ServiceResponse<ProductModel>();
        var exist = await _storeContext.Products.AnyAsync(p => p.Name.Equals(dto.Name) || p.Number.Equals(dto.Number));
        if (exist)
        {
            response.Error = true;
            response.Message = "This product already exists";
        }

        var newProduct = new ProductModel()
        {
            Number = dto.Number,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            IsWeightable = dto.IsWeightable,
            ImageUrl = dto.ImageUrl,
            Price = dto.Price,
            InStock = dto.InStock
        };
        await _storeContext.Products.AddAsync(newProduct);
        await _storeContext.SaveChangesAsync();
        return new ServiceResponse<ProductModel>
        {   Error = false,
            Message = "Product added!",
            Data = newProduct
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

        response.Error = false;
        response.Data = products;

        return response;
    }


    public async Task<ServiceResponse<ProductModel>> GetProductByNumberAsync(string number)
    {
        var response = new ServiceResponse<ProductModel>();
        var product = await _storeContext.Products.FirstOrDefaultAsync(p => p.Number.Equals(number));
        if (product is null)
        {
            response.Error = true;
            response.Message = $"Sorry, this product doesn't exist";
        }
        else
        {
            response.Data = product;
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

    public async Task<ServiceResponse<ProductModel>> GetProductByNameAsync(string name)
    {
        var response = new ServiceResponse<ProductModel>();
        var product = await _storeContext.Products.FirstOrDefaultAsync(p => p.Name.Equals(name));
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

        var productList = await _storeContext.Products.Where(p=>p.CategoryId == category.Id).ToListAsync();
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
        if (products.Count == 0)
        {
            response.Error = true;
            response.Message = $"Sorry, no result found";
        }
        else
        {
            response.Error = false;
            response.Data = products;
        };
        return response;
    }

    public async Task<ServiceResponse<ProductModel>> UpdateAsync(int id, ProductDto dto)
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
            product.Number = dto.Number;
            product.Name = dto.Name;
            product.CategoryId = dto.CategoryId;
            product.Description = dto.Description;
            product.IsWeightable = dto.IsWeightable;
            product.ImageUrl = dto.ImageUrl;
            product.Price = dto.Price;
            product.InStock = dto.InStock;
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