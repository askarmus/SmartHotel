using System.Security.Claims;

namespace SmartHotel.AuthenticationService.Services.JwtService;

public interface IJwtService
{
    string GenerateJwt(List<Claim> claims);
}
