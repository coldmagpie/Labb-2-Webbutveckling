using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAccess.Models;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ServiceResponse<List<UserModel>>> GetAllUsers();
        Task<ServiceResponse<List<UserModel>>> GetUsersByEmail(string email);
        Task<UserModel> GetUserAsync(string email);
        Task<UserModel?> GetUserById(int id);
        Task<ServiceResponse<UserModel>> AddUserAsync(UserModel user);
        //Task<ServiceResponse<List<ProductModel>>> GetUserOrderItems(int userId, int orderId);
    }
}
