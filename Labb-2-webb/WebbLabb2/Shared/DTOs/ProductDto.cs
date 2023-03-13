using System.ComponentModel.DataAnnotations;

namespace WebbLabb2.Shared.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    [Required]
    public string Number { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    [Required]
    public string? Description { get; set; } = string.Empty;
    [Required]
    public bool IsWeightable { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public bool InStock { get; set; } = false;

}