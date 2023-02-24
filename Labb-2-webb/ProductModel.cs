using System;

public class ProductModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public int ProductNumber { get; set; }
    [BsonElement]
    public string ProductName { get; set; }
    [BsonElement]
    public string ProductDescribe { get; set; }
    [BsonElement]
    public string ProductDescribe { get; set; }

}
