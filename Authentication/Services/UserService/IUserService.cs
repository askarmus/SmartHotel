using System;
using System.Threading.Tasks;
using AuthenticationService.DTO;

namespace AuthenticationService.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> GetUserDetails(string userId);
    }
}
