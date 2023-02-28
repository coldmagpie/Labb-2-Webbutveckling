using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ProductRepository : IProductRepository<ProductDto, ProductModel>
{
    private readonly IMongoCollection<ProductModel> _productCollection;
    private readonly ICategoryRepository<CategoryModel, CategoryDto> _categoryRepository;
    public ProductRepository(ICategoryRepository<CategoryModel, CategoryDto> categoryRepository)
    {
        var databaseName = "Store";
        var connectionString = $"mongodb://localhost:27017";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _productCollection = database.GetCollection<ProductModel>
            ("products", new() { AssignIdOnInsert = true });
        _categoryRepository = categoryRepository;
    }

    public async Task<ServiceResponse<ProductModel>> AddProductAsync(ProductDto dto)
    {
        var response = new ServiceResponse<ProductModel>();
        var filter = Builders<ProductModel>.Filter.Where(p => p.Name == dto.Name || p.Number == dto.Number);
        var exist = await _productCollection.Find(filter).AnyAsync();
        if (exist)
        {
            response.Success = false;
            response.Message = $"Product already exists!";
        }
       
        var newProduct = new ProductModel()
        {
            Number = dto.Number,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            IsWeightable = dto.IsWeightable,
            ImageUrl = dto.Image,
            Price = dto.Price,
            Status = dto.Status
        };
        await _productCollection.InsertOneAsync(newProduct);
        response.Success = true;
        response.Data = newProduct;
        
        return response;
    }

    public async Task <ServiceResponse<ProductModel>>DeleteProductAsync(ObjectId id)
    {
        var response = new ServiceResponse<ProductModel>();
        var filter = Builders<ProductModel>.Filter.Eq(p => p.Id, id);
        if (filter is null)
        {
            response.Success = false;
            response.Message = $"Sorry, this product doesn't exist";
        }
        else
        {
            await _productCollection.DeleteOneAsync(filter);
            response.Success = true;
            response.Message = $"Product was deleted successfully";
        }
        return response;
    }

    public async Task<ServiceResponse<ProductDto>> GetProductByNumberAsync(string number)
    {
        var response = new ServiceResponse<ProductDto>();
        var filter = Builders<ProductModel>.Filter.Eq(p => p.Number, number);
        var product = await _productCollection.Find(filter).FirstOrDefaultAsync();
        if (product is null)
        {
            response.Success = false;
            response.Message = $"Sorry, this product doesn't exist";
        }
        else
        {
            response.Success = true;
            response.Data = new ProductDto
            {
                Number = product.Number,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Description = product.Description,
                IsWeightable = product.IsWeightable,
                Price = product.Price,
                Image = product.ImageUrl,
                Status = product.Status
            };
        }

        return response;
    }

    public async Task<ServiceResponse<ProductDto>> GetProductByNameAsync(string name)
    {
        var response = new ServiceResponse<ProductDto>();
        var filter = Builders<ProductModel>.Filter.Eq(p => p.Name, name);
        var product = await _productCollection.Find(filter).FirstOrDefaultAsync();
        if (product is null)
        {
            response.Success = false;
            response.Message = $"Sorry, this product doesn't exist";
        }
        else
        {
            response.Success = true;
            response.Data = new ProductDto
            {
                Number = product.Number,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Description = product.Description,
                IsWeightable = product.IsWeightable,
                Price = product.Price,
                Image = product.ImageUrl,
                Status = product.Status
            };
        }

        return response;
    }

    public async Task<ServiceResponse<List<ProductDto>>> GetProductsByCategory(string name)
    {
        var category = await _categoryRepository.GetCategoryByName(name.ToLower());
        if (category is null)
        {
            return new ServiceResponse<List<ProductDto>>
            {
                Success = false,
                Message = "Sorry, category not found!"
            };
        }

        var productFilter = Builders<ProductModel>.Filter.Eq(p => p.CategoryId, category.Id.ToString());
        var products = await _productCollection.Find(productFilter).ToListAsync();

        return new ServiceResponse<List<ProductDto>>
        {
            Success = true,
            Data = ConvertToDto(products)
        };
    }

    public async Task<ServiceResponse<List<ProductDto>>> GetProductsBySearchText(string text)
    {
        var response = new ServiceResponse<List<ProductDto>>();
        var products = await _productCollection
            .Find(p => p.Name.ToLower().Contains(text.ToLower()) ||
                       p.Description.ToLower().Contains(text.ToLower()))
            .ToListAsync();
        if (products.Count == 0)
        {
            response.Success = false;
            response.Message = $"Sorry, no result found";
        }
        else
        {
            response.Success = true;
            response.Data = ConvertToDto(products);
        };
        return response;
    }

    public async Task<ServiceResponse<List<ProductDto>>> GetAllAsync()
    {
        var response = new ServiceResponse<List<ProductDto>>();
        var products = await _productCollection.Find(_ => true).ToListAsync();
        if (products is null)
        {
            response.Success = false;
            response.Message = $"Sorry, this is no product";
        }
  
        response.Success = true;
        response.Data = ConvertToDto(products);
        
        return response;
    }

    public async Task<ServiceResponse<ProductDto>> UpdateAsync(ObjectId id, ProductDto dto)
    {
        var response = new ServiceResponse<ProductDto>();
        var filter = Builders<ProductModel>.Filter.Eq(p => p.Id, id);
        var product = await _productCollection.Find(filter).FirstOrDefaultAsync();
        if (product is null)
        {
            response.Success = false;
            response.Message = $"Sorry, this product doesn't exist";
        }
        else
        {
            product.Number = dto.Number;
            product.Name = dto.Name;
            product.CategoryId = dto.CategoryId;
            product.Description = dto.Description;
            product.IsWeightable = dto.IsWeightable;
            product.ImageUrl = dto.Image;
            product.Price = dto.Price;
            product.Status = dto.Status;
            await _productCollection.ReplaceOneAsync(p => p.Id == id, product);
            response.Success = true;

            response.Data = new ProductDto
            {
                Number = product.Number,
                Name = product.Name,
                CategoryId = product.CategoryId.ToString(),
                Description = product.Description,
                IsWeightable = product.IsWeightable,
                Image = product.ImageUrl,
                Price = product.Price,
                Status = product.Status
            };
        }
        
        return response;
    }
    private List<ProductDto> ConvertToDto(List<ProductModel> products)
    {
        return products.Select(p => new ProductDto
        {
            Number = p.Number,
            Name = p.Name,
            CategoryId = p.CategoryId,
            Description = p.Description,
            IsWeightable = p.IsWeightable,
            Price = p.Price,
            Image= p.ImageUrl,
            Status = p.Status
        }).ToList();
    }

    private List<ProductModel> ConvertToModel(List<ProductDto> products)
    {
        return products.Select(p => new ProductModel
        {
            Number = p.Number,
            Name = p.Name,
            CategoryId = p.CategoryId,
            Description = p.Description,
            IsWeightable = p.IsWeightable,
            Price = p.Price,
            ImageUrl = p.Image,
            Status = p.Status
        }).ToList();
    }

}





 



