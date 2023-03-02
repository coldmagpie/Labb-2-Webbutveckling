using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ProductDto
{
    public string Number { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsWeightable { get; set; }
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool Status { get; set; } = false;
}



