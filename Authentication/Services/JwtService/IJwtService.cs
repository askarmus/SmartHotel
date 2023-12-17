using System.Collections.Generic;
using System.Security.Claims;

namespace AuthenticationService.Services.JwtService
{
    public interface IJwtService
    {
        string GenerateJwt(List<Claim> claims);
    }
}
