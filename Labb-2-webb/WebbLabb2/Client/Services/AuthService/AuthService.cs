using System.Net.Http.Json;
using System.Security.Claims;
using DataAccess.DataAccess.Models;
using Microsoft.AspNetCore.Components.Authorization;
using WebbLabb2.Client.Services.ProductService;
using WebbLabb2.Shared;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public event Action UserChanged;
        
        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider; 
        }
        public async Task<ServiceResponse<int>> Register(UserRegisterDto register)
        {
            var result = await _httpClient.PostAsJsonAsync("user/register", register);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto login)
        {
            var result = await _httpClient.PostAsJsonAsync("user/login", login);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }


        public async Task<ServiceResponse<bool>> UpdateProfile( int id, UserProfileDto newProfile)
        {
            var result = await _httpClient.PostAsJsonAsync($"user/update/{id}", newProfile);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int id, UserPasswordDto dto)
        {
            var result = await _httpClient.PostAsJsonAsync($"user/changepassword/{id}", dto.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task <UserProfileDto> GetUserById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<UserModel>($"userid/{id}");
            var user = new UserProfileDto()
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                Adress = result.Adress
            };
            return user;
        }
        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
    }
}
