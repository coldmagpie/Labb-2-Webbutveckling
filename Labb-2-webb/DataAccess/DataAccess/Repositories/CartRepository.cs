using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccess.DataContext;
using DataAccess.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.Repositories
{ 
    public class CartRepository:ICartRepository
    {
        public event Action? CartChanged;
        private readonly StoreContext _storeContext;

        public CartRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;   
        }

        public async Task<ServiceResponse<List<CartProductDto>>> GetCartProducts(List<CartItemDto> cartItems)
        {
            var cartProducts = new ServiceResponse<List<CartProductDto>>()
            {
                Data = new List<CartProductDto>()
            };


            foreach (var item in cartItems)
            {
                var product = await _storeContext.Products
                    .Where(p => p.Id == item.productId)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var cartProduct = new CartProductDto()
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Image = product.ImageUrl,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    IsWeightable = product.IsWeightable
                };

                cartProducts.Data.Add(cartProduct);
                cartProducts.Error = false;
            }

            return cartProducts;
        }

        //public Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
        //{
            
        //}


       
    }
}
