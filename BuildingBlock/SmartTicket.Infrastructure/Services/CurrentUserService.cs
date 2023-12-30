using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace SmartHotel.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor Get_httpContextAccessor()
        {
            return _httpContextAccessor;
        }

        public IUserSession GetCurrentUser()
        {
            if (_httpContextAccessor?.HttpContext == null)
            {
                return new UserSession();
            }

            IUserSession currentUser = new UserSession
            {
                UserId =  Guid.Parse(_httpContextAccessor?.HttpContext.User.Claims.First(x => x.Type == "UserId").Value),
                LoginName = _httpContextAccessor?.HttpContext.User.Identity.Name,
                Role = "User",
            };

            return currentUser;
        }
    }
}
