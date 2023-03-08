using DataAccess.DataAccess.Models;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> RegisterUserAsync(UserModel user, string password);
        Task<ServiceResponse<string>> LoginUserAsync(string email, string password);
        Task<ServiceResponse<bool>> UpdateProfile(int userId, UserProfileDto dto);
        Task<ServiceResponse<bool>>ChangePassword(int userId, string password);
        Task<bool> CheckUserExistsAsync(string email);
    }
}
