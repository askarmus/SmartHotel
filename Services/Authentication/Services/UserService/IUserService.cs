using System;
using System.Threading.Tasks;
using SmartHotel.AuthenticationService.DTO;

namespace SmartHotel.AuthenticationService.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetUserDetails(string userId);
    }
}
