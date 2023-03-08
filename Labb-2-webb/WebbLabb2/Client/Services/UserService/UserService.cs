using DataAccess.DataAccess.Models;
using System.Net.Http.Json;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.UserService
{
    public class UserService:IUserService
    {
        private readonly HttpClient _httpClient;

        public event Action UsersChanged;
        public string Message { get; set; } = "Loading....";
        public List<UserProfileDto> Users { get; set; } = new();
        public UserProfileDto User { get; set; }
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task GetAllUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserModel>>("/allusers");

            Users = result.Select(p => new UserProfileDto()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                Adress = p.Adress,
            }).ToList();
            UsersChanged?.Invoke();
        }
        public async Task GetUserByEmail(string email)
        {
            var result = await _httpClient.GetFromJsonAsync<UserModel>($"/useremail/{email}");

            User = new UserProfileDto()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                Adress = result.Adress,
            };
            UsersChanged?.Invoke();
        }
    }
}
