using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccess.Models;
using WebbLabb2.Shared;

namespace DataAccess.DataAccess.Interfaces
{
    public interface IUserRepository<T>
    {
        Task<ServiceResponse<List<T>>> GetAllUsers();
        Task<ServiceResponse<List<T>>> GetUserByEmail(string email);
        Task<ServiceResponse<T>> GetUserById(int id);
        //Task<ServiceResponse<List<ProductModel>>> GetUserOrderItems(int userId, int orderId);
    }
}
