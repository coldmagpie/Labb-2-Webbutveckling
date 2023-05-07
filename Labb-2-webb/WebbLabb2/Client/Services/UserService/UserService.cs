using DataAccess.DataAccess.Models;
using System.Net.Http.Json;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public event Action UsersChanged;
        public string Message { get; set; } = "No users found";
        public List<UserProfileDto>? Users { get; set; } = new();

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task GetAllUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<UserProfileDto>>>("/allusers") ?? null;
            if (result != null) Users = result.Data;
            UsersChanged?.Invoke();
        }

        public async Task GetUserByEmail(string? email)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<UserProfileDto>>>($"/useremail/{email}");
            if (result != null) Users = result.Data;
            UsersChanged?.Invoke();
        }
    }
}
