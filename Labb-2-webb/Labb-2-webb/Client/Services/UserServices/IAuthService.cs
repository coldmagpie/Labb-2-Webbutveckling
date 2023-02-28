public interface IAuthService
{
    Task<ServiceResponse<int>> Register(UserRegisterDto dto);
    Task<ServiceResponse<string>> Login(UserLoginDto dto);

}

