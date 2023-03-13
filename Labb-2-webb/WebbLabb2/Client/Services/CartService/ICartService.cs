using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.CartService
{
    public interface ICartService
    {
        Task AddProductToCart(CartItemDto dto); 
        Task<List<CartItemDto>> GetCartItems();
        Task <List<CartProductDto>> GetCartProducts();

        Task RemoveProduct(int productId);
        Task UpdateQuantity(CartProductDto product);

        Task ClearCart();
        event Action CartChanged;
    }
}
