using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ServiceResponse<int>?> Register(UserRegisterDto register)
        {
            var result = await _httpClient.PostAsJsonAsync("user/register", register);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<ServiceResponse<string>?> Login(UserLoginDto login)
        {
            var result = await _httpClient.PostAsJsonAsync("user/login", login);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }


        public async Task<ServiceResponse<bool>?> UpdateProfile(int userId, UserProfileDto newProfile)
        {
            var result = await _httpClient.PutAsJsonAsync($"user/update/{userId}", newProfile);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<bool>?> ChangePassword(int id, UserPasswordDto dto)
        {
            var result = await _httpClient.PutAsJsonAsync($"user/changepassword/{id}", dto.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<UserProfileDto> GetUserById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<UserProfileDto>($"userid/{id}");

            return result;
        }
    }
}
