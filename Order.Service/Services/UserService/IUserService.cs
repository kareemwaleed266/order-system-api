using Order.Service.Services.UserService.Dto;

namespace Order.Service.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterDto input);
        Task<UserDto> Login(LoginDto input);
    }
}
