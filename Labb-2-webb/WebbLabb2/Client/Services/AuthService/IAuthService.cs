using WebbLabb2.Shared.DTOs;
using WebbLabb2.Shared;

namespace WebbLabb2.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>?> Register(UserRegisterDto register);
        Task<ServiceResponse<string>?> Login(UserLoginDto login);
        Task<ServiceResponse<bool>?> UpdateProfile(int userId, UserProfileDto newInfo);
        Task<ServiceResponse<bool>?> ChangePassword(int id, UserPasswordDto dto);
        Task <UserProfileDto> GetUserById(int id);
    }
}
