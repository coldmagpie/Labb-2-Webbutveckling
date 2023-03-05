namespace WebbLabb2.Shared.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool IsWeightable { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public bool Status { get; set; } = false;
}