using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class UserModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    [BsonElement]
    public string FirstName { get; set; }
    [BsonElement]
    public string LastName { get; set; }
    [BsonElement]
    public string Email { get; set; }
    [BsonElement]
    public string PhoneNumber { get; set; }
    [BsonElement]
    public string Adress { get; set; }
    [BsonElement]
    public byte[] PasswordHash { get; set; }
    [BsonElement]
    public byte[] PasswordSalt { get; set; }
}

