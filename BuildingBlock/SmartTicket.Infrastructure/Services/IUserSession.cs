
namespace SmartHotel.Infrastructure.Services;

public interface IUserSession
{
    string LoginName { get; set; }
    Guid UserId { get; set; }
}
