namespace SmartHotel.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor) : ICurrentUserService
{

    public IHttpContextAccessor Get_httpContextAccessor()
    {
        return _httpContextAccessor;
    }

    public IUserSession GetCurrentUser()
    {
        if (_httpContextAccessor?.HttpContext != null)
        {
           return  new UserSession
            {
                UserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "UserId").Value),
                LoginName = _httpContextAccessor.HttpContext.User.Identity.Name,
                Role = "User",
            };
        }

        return new UserSession();

    }
}
