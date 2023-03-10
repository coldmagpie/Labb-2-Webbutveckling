
namespace DataAccess.DataAccess.Models;

public class ProductModel
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string? Description { get; set; }
    public bool IsWeightable { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public bool InStock { get; set; }
}