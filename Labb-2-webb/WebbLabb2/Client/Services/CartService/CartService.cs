using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;
using Blazored.LocalStorage;
using WebbLabb2.Shared;

namespace WebbLabb2.Client.Services.CartService
{
    public class CartService:ICartService
    {
        public ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;


        public event Action? CartChanged;
        public CartService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }
        public async Task AddProductToCart(CartItemDto dto)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (cart == null)
            {
                cart = new List<CartItemDto>();
            }

            var existing = cart.FirstOrDefault(c=>c.productId == dto.productId);
            if (existing is not null)
            {
                existing.Quantity+= existing.Quantity;
            }
            else
            {
                cart.Add(dto);
            }
            await _localStorage.SetItemAsync("cart", cart);
            CartChanged?.Invoke();
        }

        public async Task<List<CartItemDto>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (cart == null)
            {
                cart = new List<CartItemDto>();
            }
            return cart;
        }

        public async Task<List<CartProductDto>> GetCartProducts()
        {
            var cartItems = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (cartItems == null)
            {
                return new List<CartProductDto>();
            } 
            var response = await _httpClient.PostAsJsonAsync("/cartproducts", cartItems);
            var result = await response.Content.ReadFromJsonAsync<List<CartProductDto>>();
            return result;
        }

        public async Task RemoveProduct(int productId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (cart is null)
            {
                return;
            }
            var item = cart.FirstOrDefault(i=>i.productId == productId);
            if (item is null)
            {
                return;
            }
            cart.Remove(item);
            await _localStorage.SetItemAsync("cart", cart);
            CartChanged.Invoke();

        }

        public async Task UpdateQuantity(CartProductDto product)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (cart is null)
            {
                return;
            }
            var item = cart.FirstOrDefault(i => i.productId == product.ProductId);
            if (item is null)
            {
                return;
            }
            item.Quantity = product.Quantity;
            await _localStorage.SetItemAsync("cart", cart);
        }
        public async Task ClearCart()
        {
            await _localStorage.RemoveItemAsync("cart");
        }
    }
}
