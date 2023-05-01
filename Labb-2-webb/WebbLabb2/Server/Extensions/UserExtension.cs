using System.Security.Claims;
using DataAccess.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbLabb2.Shared.DTOs;
using Microsoft.AspNetCore.Http;

using IAuthService = WebbLabb2.Server.Services.AuthService.IAuthService;
using WebbLabb2.Shared;
using System.Net.Http;
using DataAccess.DataAccess.Repositories.Interfaces;
using DataAccess.DataAccess.UnitOfWork;

namespace WebbLabb2.Server.Extensions;

public static class UserExtension
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/user/register", RegisterUserHandlerAsync);
        app.MapPost("/user/login", LoginUserHandlerAsync);
        app.MapPut("/user/update/{id}", UpdateHandlerAsync);
        app.MapPut("/user/changepassword/{id}", ChangePasswordAsync);
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

    private static async Task<IResult> RegisterUserHandlerAsync(IAuthService authService, UserRegisterDto newUser)
    {
        var model = new UserModel
        {
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email,
            PhoneNumber = newUser.PhoneNumber,
            Adress = newUser.Adress
        };
        var response = await authService.RegisterUserAsync(model,newUser.Password);
        return response.Error ? Results.BadRequest("register failed") : Results.Ok(response);
    }


    public static async Task<IResult> UpdateHandlerAsync(IAuthService authService, int id, [FromBody]UserProfileDto newProfile)
    {
        var response = await authService.UpdateProfile(id, newProfile);

        return response.Error ? Results.BadRequest("profile update failed") : Results.Ok(response);
    }

    public static async Task<IResult> ChangePasswordAsync(IAuthService authService, [FromBody] string password, int id)
    {
        var response = await authService.ChangePassword(id, password);
        return response.Error ? Results.BadRequest("change password failed") : Results.Ok(response);
    }
    private static async Task<IResult> GetUserById(IUnitOfWork unitOfWork, int id)
    {
        var response = await unitOfWork.UserRepository.GetUserById(id);
        return response is null ? Results.BadRequest("user not found") : Results.Ok(response);
    }
    private static async Task<IResult> GetUserByEmail(IUnitOfWork unitOfWork,string email)
    {
        var response = await unitOfWork.UserRepository.GetUserAsync(email);
        return response is null ? Results.BadRequest("user not found") : Results.Ok(response);
    }
    private static async Task<IResult> GetAllUsers(IUnitOfWork unitOfWork)
    {
        var response = await unitOfWork.UserRepository.GetAllUsers();
        return response is null ? Results.BadRequest("user not found") : Results.Ok(response);
    }
}

