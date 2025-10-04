using ServicesLayer.HandleResponses.CommanResponse;
using ServicesLayer.Services.UserService.Dtos;

namespace ServicesLayer.Services.UserService
{
    public interface IUserService
    {
        Task<CommonResponse<UserDto>> LoginAsync(LoginDto loginDto);
        Task<CommonResponse<UserDto>> RegisterAync(RegisterDto registerDto);
    }
}
