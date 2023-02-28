
public interface IAuthService
{
    Task<ServiceResponse<int>> RegisterUserAsync(UserModel user, string password);
    Task<bool> CheckUserExistsAsync(string email);
    Task<ServiceResponse<string>> LoginUserAsync(string email, string password);
}

