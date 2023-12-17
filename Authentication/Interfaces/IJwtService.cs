using System.Collections.Generic;
using System.Security.Claims;

namespace Authentication.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwt(List<Claim> claims);
    }
}
