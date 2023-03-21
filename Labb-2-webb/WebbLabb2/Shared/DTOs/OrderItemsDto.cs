namespace WebbLabb2.Shared.DTOs
{
    public class OrderItemsDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public double PriceEach { get; set; }
    }
}
