using System.Security.Claims;
using DataAccess.DataAccess.Interfaces;
using DataAccess.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbLabb2.Shared.DTOs;
using Microsoft.AspNetCore.Http;

using IAuthService = WebbLabb2.Server.Services.AuthService.IAuthService;
using WebbLabb2.Shared;
using System.Net.Http;

namespace WebbLabb2.Server.Extensions;
public static class UserExtension
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/user/register", RegisterUserHandlerAsync);
        app.MapPost("/user/login", LoginUserHandlerAsync);

        app.MapPost("/user/update/{id}", UpdateHandlerAsync);
        app.MapPost("/user/changepassword/{id}", ChangePasswordAsync);
        app.MapGet("/userid/{id}", GetUserById);
        app.MapGet("/useremail/{email}", GetUserByEmail);
        app.MapGet("/allusers", GetAllUsers);

        return app;
    }

    private static async Task<IResult> LoginUserHandlerAsync(IAuthService authService, UserLoginDto dto)
    {
        var response = await authService.LoginUserAsync(dto.Email, dto.Password);
        return response.Error ? Results.BadRequest(response) : Results.Ok(response);
    }

    private static async Task<IResult> RegisterUserHandlerAsync(IAuthService authService, UserRegisterDto dto)
    {
        var model = new UserModel
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Adress = dto.Adress
        };
        var response = await authService.RegisterUserAsync(model, dto.Password);
        return response.Error ? Results.BadRequest(response) : Results.Ok(response);
    }

    
    public static async Task<IResult> UpdateHandlerAsync(IAuthService authService, int userId, UserProfileDto newProfile)
    {
        var response = await authService.UpdateProfile(userId, newProfile);

        if (response.Error)
        {
            return Results.BadRequest(response);
        }
        return Results.Ok(response);
    }

    public static async Task<IResult> ChangePasswordAsync(IAuthService authService,[FromBody] string password, int id)
    {
        var response = await authService.ChangePassword(id, password);
        if (response.Error)
        {
            return Results.BadRequest(response);
        }

        return Results.Ok(response);
    }
    private static async Task<IResult> GetUserById(IUserRepository<UserModel> userRepository, int id)
    {
        var response = await userRepository.GetUserById(id);
        return response.Error ? Results.BadRequest(response) : Results.Ok(response.Data);
    }
    private static async Task<IResult> GetUserByEmail(IUserRepository<UserModel> userRepository,string email)
    {
        var response = await userRepository.GetUserByEmail(email);
        return response.Error ? Results.BadRequest(response) : Results.Ok(response.Data);
    }
    private static async Task<IResult> GetAllUsers(IUserRepository<UserModel> userRepository)
    {
        var response = await userRepository.GetAllUsers();
        return response.Error ? Results.BadRequest(response) : Results.Ok(response.Data);
    }
}

