

namespace SmartHotel. BookingService.CQRS.Queries.GetRoomAvailabilities.Response;

public class GetRoomAvailabilitiesQueryResponse
{
    public List<RoomAvailabilityDto> RoomAvailabilities { get; set; }
    public int ResultCount { get; set; }

    public GetRoomAvailabilitiesQueryResponse(List<RoomAvailabilityDto> roomAvailabilities, int resultCount)
    {
        RoomAvailabilities = roomAvailabilities;
        ResultCount = resultCount;
    }
}


public class RoomAvailabilityDto
{
    public int RoomAvailabilityId { get; set; }
    public int RoomId { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime BookingDate { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
}