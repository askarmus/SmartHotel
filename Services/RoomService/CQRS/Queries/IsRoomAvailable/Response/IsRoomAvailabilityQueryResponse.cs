
namespace SmartHotel. BookingService.CQRS.Queries.IsRoomAvailable.Response;
public class IsRoomAvailabilityQueryResponse
{
    public IsRoomAvailabilityQueryResponse(bool isAvailable)
    {
        isAvailable = isAvailable;
    }
    public int isAvailable { get; }
}