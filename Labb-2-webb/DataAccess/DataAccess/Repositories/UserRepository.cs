using DataAccess.DataAccess.DataContext;
using DataAccess.DataAccess.Interfaces;
using DataAccess.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Shared;

namespace DataAccess.DataAccess.Repositories
{
    public class UserRepository : IUserRepository<UserModel>
    {
        private readonly StoreContext _storeContext;

        public UserRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<ServiceResponse<List<UserModel>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<UserModel>>();
            var users = await _storeContext.Users.ToListAsync();
            if (users is null)
            {
                response.Error = true;
                response.Message = "No user found";
            }

            response.Error = false;
            response.Data = users;
            
            return response;
        }
        public async Task<ServiceResponse<UserModel>> GetUserById(int userId)
        {
            var user = await _storeContext.Users.FirstOrDefaultAsync(u=>u.Id == userId);
            if (user is null)
            {
                return new ServiceResponse<UserModel>
                {
                    Error = true,
                    Message = "User not found."
                };
            }
            return new ServiceResponse<UserModel> { Data = user, Error = false };
        }

        public async Task<ServiceResponse<List<UserModel>>> GetUserByEmail(string email)
        {
            var response = new ServiceResponse<List<UserModel>>();
            var user = await _storeContext.Users.Where(u => u.Email.Contains(email)).ToListAsync();
            if (user is null)
            {
                response.Error = true;
                response.Message = "No user found";
            }
            else
            {
                response.Error = false;
                response.Data = user;
            }
            return response;
        }
    }
}
