
namespace SmartHotel.BookingService.Repository
{
    public interface IBookingRepository
    {
        Task<Entities.Booking> GetBookingAsync(int bookingId);
        Task UpdateBookingStatusAsync(Entities.Booking booking);
        Task<int> CreateBookingAsync(Entities.Booking booking);
    }
}
