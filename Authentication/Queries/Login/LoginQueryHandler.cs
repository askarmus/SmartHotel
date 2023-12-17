using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Authentication.User.Application.Exceptions;
using Authentication.Queries.Login.Responses;
using Authentication.Entities;
using Authentication.Interfaces;

namespace Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;

        public LoginQueryHandler(UserManager<AppUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new LoginException();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role,"User"),
                new Claim("UserId",user.Id)
            };

            return new LoginResponse(_jwtService.GenerateJwt(claims));
        }
    }
}