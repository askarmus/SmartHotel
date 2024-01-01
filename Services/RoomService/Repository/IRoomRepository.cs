
using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities;
using SmartHotel.RoomService.Data.Entities;

namespace SmartHotel. BookingService.Repository
{
    public interface IRoomRepository
    {
        Task<bool> IsRoomAvailable(int roomId, DateTime bookingDate);
        Task<int> CreateBookingStatus(int roomId, DateTime bookingDatee);
        (IEnumerable<RoomAvailability>, int) GetAvailabilities(GetRoomAvailabilitiesQuery query);
    }
}
