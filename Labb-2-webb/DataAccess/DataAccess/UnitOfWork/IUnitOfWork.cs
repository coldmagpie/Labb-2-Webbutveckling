using DataAccess.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccess.Models;
using DataAccess.DataAccess.Repositories;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICartRepository CartRepository { get; }
        public IUserRepository UserRepository { get; }
        public Task<int> Save();
        public void Dispose();
    }
}
