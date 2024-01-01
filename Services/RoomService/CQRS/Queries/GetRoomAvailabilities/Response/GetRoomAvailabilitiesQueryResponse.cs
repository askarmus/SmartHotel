
using Service.Shared.Enum;

namespace SmartHotel. BookingService.CQRS.Queries.GetRoomAvailabilities.Response
{
    public class GetRoomAvailabilitiesQueryResponse
    {
        public int RoomAvailabilityId { get; set; }
        public int RoomId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime BookingDate { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
    }
}