using DataAccess.DataAccess.DataContext;
using DataAccess.DataAccess.Models;
using DataAccess.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace DataAccess.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
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
            else
            {
                response.Error = false;
                response.Data = users;
            }
            return response;
        }

        public async Task<UserModel> GetUserAsync(string email)
        {
            var user = await _storeContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<UserModel?> GetUserById(int userId)
        {
            var user = await _storeContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<ServiceResponse<UserModel>> AddUserAsync(UserModel newUser)
        {
            var response = new ServiceResponse<UserModel>();
            var exist = await _storeContext.Users.AnyAsync(p => p.Email.Equals(newUser.Email));
            if (exist)
            {
                response.Error = true;
                response.Message = "The email already exists";
            }

            await _storeContext.Users.AddAsync(newUser);
            await _storeContext.SaveChangesAsync();
            return new ServiceResponse<UserModel>
            {
                Error = false,
                Message = "user registered!",
                Data = newUser
            };
        }

        public async Task<ServiceResponse<List<UserModel>>> GetUsersByEmail(string email)
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
