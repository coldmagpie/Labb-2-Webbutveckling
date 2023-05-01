using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccess.DataContext;
using DataAccess.DataAccess.Models;
using DataAccess.DataAccess.Repositories;
using DataAccess.DataAccess.Repositories.Interfaces;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.UnitOfWork
{
    public class UnitOfWork:IDisposable, IUnitOfWork
    {
        private readonly StoreContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(StoreContext context, IOrderRepository orderRepository, IProductRepository productRepository, ICategoryRepository categoryRepository, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;   
        }

        public IProductRepository ProductRepository => _productRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public ICartRepository CartRepository => _cartRepository;
        public IUserRepository UserRepository => _userRepository;

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
