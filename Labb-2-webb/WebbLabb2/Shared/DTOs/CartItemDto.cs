namespace WebbLabb2.Shared.DTOs
{
    public class CartItemDto
    {
        public int productId { get; set; }
        public int Quantity { get; set; } = 1;
        public double Price { get; set; }
    }
}
