
namespace SmartHotel.Infrastructure.Services;

public class UserSession : IUserSession
{
    public string LoginName { get; set; }

    public Guid UserId { get; set; }
    public string Role { get; internal set; }
}
