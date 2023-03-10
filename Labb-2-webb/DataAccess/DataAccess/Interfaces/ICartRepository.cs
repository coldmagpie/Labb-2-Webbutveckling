using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.Interfaces
{
    public interface ICartRepository
    {
        event Action CartChanged;
        //Task AddToCart(ProductDto dto);
        Task<ServiceResponse<List<CartProductDto>>> GetCartProducts(List<CartItemDto> cartItems);
        //Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId);
    }
}
