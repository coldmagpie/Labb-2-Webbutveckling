using MongoDB.Bson;
using MongoDB.Driver;

public class CategoryRepository:ICategoryRepository<CategoryModel, CategoryDto>
{
    private readonly IMongoCollection<CategoryModel> _categoryCollection;
    public CategoryRepository()
    {
        var databaseName = "Store";
        var connectionString = $"mongodb://localhost:27017";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _categoryCollection = database.GetCollection<CategoryModel>
            ("categories", new() { AssignIdOnInsert = true });
    }
    public async Task<ServiceResponse<CategoryModel>> AddCategoryAsync(CategoryDto category)
    {
        var response = new ServiceResponse<CategoryModel>();
        var filter = Builders<CategoryModel>.Filter.Where(p => p.Name == category.Name);
        var exist = await _categoryCollection.Find(filter).AnyAsync();
        if (exist)
        {
            response.Success = false;
            response.Message = $"Category already exists!";
            return response;
        }
        else
        {
            var newCategory = new CategoryModel()
            {
                Name = category.Name,
            };
            await _categoryCollection.InsertOneAsync(newCategory);
            response.Success = true;
            response.Data = newCategory;
        }
        return response;
    }

    public async Task<ServiceResponse<CategoryModel>> DeleteCategoryAsync(ObjectId id)
    {
        var response = new ServiceResponse<CategoryModel>();
        var filter = Builders<CategoryModel>.Filter.Eq(p => p.Id, id);
        if (filter is null)
        {
            response.Success = false;
            response.Message = $"Sorry, this category doesn't exist";
        }
        else
        {
            await _categoryCollection.DeleteOneAsync(filter);
            response.Success = true;
            response.Message = $"Product was deleted successfully";
        }
        return response;
    }

    public async Task<ServiceResponse<CategoryDto>> UpdateCategoryAsync(ObjectId id, CategoryDto dto)
    {
        var response = new ServiceResponse<CategoryDto>();
        var filter = Builders<CategoryModel>.Filter.Eq(p => p.Id, id);
        var category = await _categoryCollection.Find(filter).FirstOrDefaultAsync();
        if (category is null)
        {
            response.Success = false;
            response.Message = $"Sorry, this category doesn't exist";
        }
        else
        {
           
            category.Name = dto.Name;
            await _categoryCollection.ReplaceOneAsync(p => p.Id == id, category);
            response.Success = true;
            response.Data = new CategoryDto(){Name = category.Name};
        }

        return response;
    }
    public async Task<CategoryModel> GetCategoryByName(string name)
    {
        var filter = Builders<CategoryModel>.Filter.Eq(c => c.Name, name);
        var category = await _categoryCollection.Find(filter).FirstOrDefaultAsync();
        return category;

    }
}

