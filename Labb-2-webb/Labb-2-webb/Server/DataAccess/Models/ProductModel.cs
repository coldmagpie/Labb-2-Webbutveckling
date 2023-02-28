using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ProductModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public string Number { get; set; }
    [BsonElement]
    public string Name { get; set; }
    [BsonElement]
    public string ImageUrl { get; set; }
    [BsonElement]
    public string Description { get; set; }
    [BsonElement]
    public bool IsWeightable { get; set; }
    [BsonElement]
    public decimal Price { get; set; }
    [BsonElement]
    public string CategoryId { get; set; }
    [BsonElement]
    public bool Status { get; set; }
}


