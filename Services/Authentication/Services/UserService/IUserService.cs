namespace SmartHotel.AuthenticationService.Services.UserService;

public interface IUserService
{
    Task<UserDto> GetUserDetails(string userId);
}
