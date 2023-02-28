using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class CategoryModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public string Name { get; set; }
}

