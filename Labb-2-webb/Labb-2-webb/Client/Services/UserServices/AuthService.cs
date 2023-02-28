using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }
    public async Task<ServiceResponse<int>> Register(UserRegisterDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("user/register", dto);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }

    public async Task<ServiceResponse<string>> Login(UserLoginDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("user/login", dto);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }
}

