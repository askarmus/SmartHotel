
namespace BookingService.Repository
{
    public interface IRoomRepository
    {
        Task<bool> IsRoomAvailable(int roomId, DateTime bookingDate);
        Task<int> CreateBookingStatus(int roomId, DateTime bookingDatee);
    }
}
