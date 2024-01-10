namespace SmartHotel.Infrastructure.Services;

public interface ICurrentUserService
{
    IUserSession GetCurrentUser();
}
