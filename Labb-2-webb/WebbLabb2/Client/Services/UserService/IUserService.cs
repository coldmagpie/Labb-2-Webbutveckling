using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.UserService
{
    public interface IUserService
    {
        string Message { get; set; }
        public List<UserProfileDto>? Users { get; set; }
        public Task GetAllUsers();
        public Task GetUserByEmail(string? email = null);
        event Action UsersChanged;
    }
}
