using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ProductRepository : IProductRepository<ProductDto, ProductModel>
{
    private readonly IMongoCollection<ProductModel> _productCollection;
 

    public ProductRepository()
    {
        var host = "localhost";
        var databaseName = "Products";
        var connectionString = $"mongodb://{host}:27017";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _productCollection = database.GetCollection<ProductModel>
            ("products", new() { AssignIdOnInsert = true });
    }

    public async Task<ServiceResponse<ProductModel>> AddAsync(ProductDto dto)
    {
        var response = new ServiceResponse<ProductModel>();
        var filter = Builders<ProductModel>.Filter.Where(p => p.Name == dto.Name || p.Number == dto.Number);
        var exist = await _productCollection.Find(filter).AnyAsync();
        if (exist)
        {
            response.Success = false;
            response.Message = $"Product already exists!";
            return response;
        }
        else
        {
            var newProduct = new ProductModel()
            {
                Number = dto.Number,
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                Describe = dto.Describe,
                ImageUrl = dto.Image,
                Price = dto.Price,
                Status = dto.Status
            };
            await _productCollection.InsertOneAsync(newProduct);
            response.Success = true;
            response.Data = newProduct;
        }
        return response;
    }

    public async Task <ServiceResponse<ProductModel>>DeleteAsync(object id)
    {
        var response = new ServiceResponse<ProductModel>();
        var filter = Builders<ProductModel>.Filter.Eq(p => p.Id, id);
        var product = await _productCollection.Find(filter).FirstOrDefaultAsync();
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


    public async Task<ServiceResponse<ProductModel>> GetProductByNumberAsync(string number)
    {
        var response = new ServiceResponse<ProductModel>();
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
            response.Data = product;
        }

        return response;
    }

    public async Task<ServiceResponse<ProductModel>> GetProductByNameAsync(string name)
    {
        var response = new ServiceResponse<ProductModel>();
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
            response.Data = product;
        }

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetAllAsync()
    {
        var response = new ServiceResponse<IEnumerable<ProductModel>>();
        var products = await _productCollection.Find(_ => true).ToListAsync();
        if (products is null)
        {
            response.Success = false;
            response.Message = $"Sorry, this is no product";
        }
        {
            response.Success = true;
            response.Data = products;
        };
        return response;
    }

    public async Task<ServiceResponse<ProductModel>> UpdateAsync(object id, ProductDto dto)
    {

        var response = new ServiceResponse<ProductModel>();
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
            product.Describe = dto.Describe;
            product.ImageUrl = dto.Image;
            product.Price = dto.Price;
            product.Status = dto.Status;
            await _productCollection.ReplaceOneAsync(p => p.Id == ObjectId.Parse((string)id), product);
            response.Success = true;
            response.Data = product;
        }

        return response;
    }

}





 



